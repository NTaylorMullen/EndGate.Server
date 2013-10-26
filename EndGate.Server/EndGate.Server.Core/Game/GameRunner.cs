using System;
using System.Collections.Concurrent;
using EndGate.Server.Loopers;

namespace EndGate.Server
{
    internal class GameRunner
    {
        private static Lazy<GameRunner> _instance = new Lazy<GameRunner>(() => new GameRunner());
        private ConcurrentDictionary<long, GameLoopCallbacks> _callbacks;
        private Looper _loop;

        public GameRunner()
        {
            _callbacks = new ConcurrentDictionary<long, GameLoopCallbacks>();
        }

        public static GameRunner Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        public GameRegistration Register(Game game)
        {
            var callbacks = CreateAndCacheCallbacks(game);

            // Try to start the loop prior to adding our games callback.  This callback may be the first, hence the "Try"
            TryLoopStart();

            // Add our callback to the game loop (which is now running), it will now be called on an interval dictated by the callbacks
            _loop.AddCallback(callbacks.UpdateLoopCallback);
            _loop.AddCallback(callbacks.PushLoopCallback);

            // Updating the "updateRate" and "pushRate" is an essential element to the game configuration.
            // If a game is running slowly we need to be able to slow down the update rate.
            return new GameRegistration
            {
                UpdateRateSetter = CreateRateSetter(callbacks.UpdateLoopCallback),
                PushRateSetter = CreateRateSetter(callbacks.PushLoopCallback)
            };
        }

        public void Unregister(Game game)
        {
            GameLoopCallbacks callbacks;
            _callbacks.TryRemove(game.ID, out callbacks);
            _loop.RemoveCallback(callbacks.UpdateLoopCallback);
            _loop.RemoveCallback(callbacks.PushLoopCallback);

            TryLoopStop();
        }

        private void TryLoopStart()
        {
            if (_callbacks.Count == 1)
            {
                _loop = new Looper();
                _loop.Start();
            }
        }

        private void TryLoopStop()
        {
            if (_callbacks.Count == 0 && _loop != null)
            {
                _loop.Dispose();
                _loop = null;
            }
        }

        private GameLoopCallbacks CreateAndCacheCallbacks(Game game)
        {
            var callbacks = new GameLoopCallbacks
            {
                UpdateLoopCallback = new TimedCallback
                {
                    Callback = game.PrepareUpdate
                },
                PushLoopCallback = new TimedCallback
                {
                    Callback = game.PreparePush
                }
            };

            // Add the callback to the callback cache
            _callbacks.TryAdd(game.ID, callbacks);

            return callbacks;
        }

        private Action<int> CreateRateSetter(TimedCallback callback)
        {
            return (rate) =>
            {
                callback.Fps = rate;
            };
        }
    }
}
