using System;
using System.Threading;
using EndGate.Server.Collision;
using EndGate.Server;

namespace EndGate.Server
{
    /// <summary>
    /// Provides collision, game, and server logic code.
    /// </summary>
    public abstract class Game : IUpdateable, IDisposable
    {
        private GameTime _gameTime;

        /// <summary>
        /// The game configuration.  Used to modify the update and push intervals.
        /// </summary>
        public GameConfiguration Configuration;
        /// <summary>
        /// A collision manager which is used to actively detect collisions between monitored <see cref="Collidable"/>'s.
        /// </summary>
        public CollisionManager CollisionManager;

        internal static long GameIDs = 0;
        internal long ID = 0;

        /// <summary>
        /// Initiates a new game object.
        /// </summary>
        public Game()
        {
            _gameTime = new GameTime();
            ID = Interlocked.Increment(ref GameIDs);
            CollisionManager = new CollisionManager();

            Configuration = new GameConfiguration(GameRunner.Instance.Register(this));
        }

        internal void PrepareUpdate()
        {
            _gameTime.Update();

            CollisionManager.Update(_gameTime);
            Update(_gameTime);
        }

        internal void PreparePush()
        {
            Push();
        }

        /// <summary>
        /// Triggered on a regular interval defined by the <see cref="GameConfiguration"/>.  Should be overridden to run game logic.
        /// </summary>
        /// <param name="gameTime">The global game time object.  Used to represent total time running and used to track update interval elapsed speeds.</param>
        public virtual void Update(GameTime gameTime)
        {
        }

        /// <summary>
        /// Triggered on a regular interval defined by the <see cref="GameConfiguration"/>.  Should be overridden to push data over the wire to connected clients.
        /// </summary>
        public virtual void Push()
        {
        }

        /// <summary>
        /// Disposes the game and stops the update and push intervals.
        /// </summary>
        public virtual void Dispose()
        {
            GameRunner.Instance.Unregister(this);
        }
    }
}
