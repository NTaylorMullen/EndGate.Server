using System;
using System.Collections.Generic;
using System.Linq;
using EndGate.Server;

namespace EndGate.Server.Collision
{
    /// <summary>
    /// Defines a manager that will check for collisions between objects that it is monitoring.
    /// </summary>
    public class CollisionManager : IUpdateable
    {
        private Dictionary<long, Collidable> _collidables;
        private Queue<Collidable> _collidableAdditions;
        private Queue<Collidable> _collidableRemovals;
        private object _collidableAddLock;
        private object _collidableRemoveLock;
        private bool _enabled;

        /// <summary>
        /// Creates a new instance of CollisionManager.
        /// </summary>
        public CollisionManager()
        {
            _collidables = new Dictionary<long, Collidable>();
            _collidableAdditions = new Queue<Collidable>();
            _collidableRemovals = new Queue<Collidable>();
            _collidableAddLock = new object();
            _collidableRemoveLock = new object();
            _enabled = false;

            OnCollision = (first, second) => { };
        }

        /// <summary>
        /// Gets an event that is triggered when a collision happens among two of the monitored objects.
        /// </summary>
        public event Action<Collidable, Collidable> OnCollision;

        /// <summary>
        /// Monitors the provided collidable and will trigger its Collided function and OnCollision event whenever a collision occurs with it and another Collidable.  If the provided collidable gets disposed it will automatically become unmonitored.
        /// </summary>
        /// <param name="obj">Collidable to monitor.</param>
        public void Monitor(Collidable obj)
        {
            lock (_collidableAddLock)
            {
                _enabled = true;
                _collidableAdditions.Enqueue(obj);
            }
        }

        /// <summary>
        /// Unmonitors the provided collidable.  The Collided function and OnCollision event will no longer be triggered when an actual collision may have occurred.  Disposing a monitored collidable will automatically be unmonitored.
        /// </summary>
        /// <param name="obj">Collidable to unmonitor.</param>
        public void Unmonitor(Collidable obj)
        {
            lock (_collidableRemoveLock)
            {
                _collidableRemovals.Enqueue(obj);
            }
        }

        /// <summary>
        /// Checks for collisions within its monitored objects.  Games CollisionManager's automatically have their Update functions called at the beginning of each update loop.
        /// </summary>
        /// <param name="gameTime">The current game time object.</param>
        public void Update(GameTime gameTime)
        {
            if (_enabled)
            {
                DrainAddQueue();
                DrainRemoveQueue();

                List<Collidable> collidables = _collidables.Values.ToList();
                Collidable first, second;

                for (var i = 0; i < collidables.Count; i++)
                {
                    first = collidables[i];

                    for (var j = i + 1; j < collidables.Count; j++)
                    {
                        second = collidables[j];

                        if (first.IsCollidingWith(second))
                        {
                            first.Collided(new CollisionData(second));
                            second.Collided(new CollisionData(first));
                            OnCollision(first, second);
                        }
                    }
                }
            }
        }

        private void DrainAddQueue()
        {
            lock (_collidableAddLock)
            {
                Collidable obj;

                while (_collidableAdditions.Count != 0)
                {
                    obj = _collidableAdditions.Dequeue();

                    obj.OnDisposed += a =>
                    {
                        Unmonitor(a);
                    };

                    _collidables.Add(obj.ID, obj);
                }
            }
        }

        private void DrainRemoveQueue()
        {
            lock (_collidableRemoveLock)
            {
                while (_collidableRemovals.Count != 0)
                {
                    _collidables.Remove(_collidableRemovals.Dequeue().ID);
                }
            }
        }
    }
}
