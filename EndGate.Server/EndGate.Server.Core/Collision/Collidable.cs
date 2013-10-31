using System;
using System.Threading;
using EndGate.Server.BoundingObject;

namespace EndGate.Server.Collision
{
    /// <summary>
    /// Defines a collidable object that can be used to detect collisions with other objects.
    /// </summary>
    public class Collidable : IDisposable
    {
        private static long _collidableIDs = 0;

        private long _disposed;

        /// <summary>
        /// Creates a new instance of Collidable.
        /// </summary>
        /// <param name="bounds">Initial bounds for the Collidable.</param>
        public Collidable(Bounds2d bounds)
        {
            _disposed = 0;

            Bounds = bounds;
            ID = Interlocked.Increment(ref _collidableIDs);

            // Set a default collision event so we don't have to check if its null
            OnCollision = obj => { };
            OnDisposed = obj => { };
        }

        /// <summary>
        /// Gets an event that is triggered when a collision occurs.
        /// </summary>
        public event Action<CollisionData> OnCollision;
        /// <summary>
        /// Gets an event that is triggered when the collidable is disposed.
        /// </summary>
        public event Action<Collidable> OnDisposed;

        /// <summary>
        /// Gets or sets the Bounds of the collidable.
        /// </summary>
        public Bounds2d Bounds { get; set; }

        internal long ID { get; private set; }

        /// <summary>
        /// Determines if the provided collidable is colliding with this Collidable.
        /// </summary>
        /// <param name="other">Collidable to check collision with.</param>
        /// <returns>Whether a collision is present.</returns>
        public bool IsCollidingWith(Collidable other)
        {
            return this.Bounds.Intersects(other.Bounds);
        }

        /// <summary>
        /// Triggers the OnCollision event.  Can also be overridden from derived classes to be called when a collision occurs if the collidable is being used with a CollisionManager.
        /// </summary>
        /// <param name="data">Collision information related to the collision.</param>
        public void Collided(CollisionData data)
        {
            OnCollision(data);
        }

        /// <summary>
        /// Triggers the OnDisposed event.  If this Collidable is used with a <see cref="CollisionManager"/> it will be unmonitored when disposed.
        /// </summary>
        public virtual void Dispose()
        {
            if (Interlocked.Exchange(ref _disposed, 1) == 0)
            {
                OnDisposed(this);
            }
            else
            {
                throw new InvalidOperationException("Cannot dispose collidable twice.");
            }
        }
    }
}