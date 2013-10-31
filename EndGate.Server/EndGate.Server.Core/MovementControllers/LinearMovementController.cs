using System;

namespace EndGate.Server.MovementControllers
{
    /// <summary>
    /// A <see cref="MovementController"/> that moves objects at a linear rate.
    /// </summary>
    /// <example>If constructed with a move speed of 100, that means all the provided moveables will be moved at 100 pixels per second depending on where the LinearMovementController is trying to move.</example>
    public class LinearMovementController : MovementController
    {
        private bool _rotateWithMovements;
        private double _moveSpeed;
        private Action<LinearMovementController> _velocityUpdater;

        public LinearMovementController(IMoveable[] moveables, double moveSpeed)
            : this(moveables, moveSpeed, true, true)
        { }
        public LinearMovementController(IMoveable[] moveables, double moveSpeed, bool rotateWithMovements)
            : this(moveables, moveSpeed, rotateWithMovements, true)
        { }
        public LinearMovementController(IMoveable[] moveables, double moveSpeed, bool rotateWithMovements, bool multiDirectional)
            : base(moveables)
        {
            Moving = new LinearDirections();
            _moveSpeed = moveSpeed;
            _rotateWithMovements = rotateWithMovements;

            OnMove = _ => { };

            if (multiDirectional)
            {
                _velocityUpdater = UpdateVelocityWithMultiDirection;
            }
            else
            {
                _velocityUpdater = UpdateVelocityNoMultiDirection;
            }
        }

        public event Action<IMoveEvent> OnMove;

        public LinearDirections Moving { get; private set; }
        public double MoveSpeed
        {
            get
            {
                return this._moveSpeed;
            }
            set
            {
                this._moveSpeed = value;
                this._velocityUpdater(this);
            }
        }

        public bool IsMovingInDirection(string direction)
        {
            switch (direction)
            {
                case "Left":
                    return Moving.Left;
                case "Right":
                    return Moving.Right;
                case "Up":
                    return Moving.Up;
                case "Down":
                    return Moving.Down;
            }

            throw new InvalidOperationException(direction + " is an unknown direction.");
        }

        public void StartMoving(string direction)
        {
            Move(direction, true);
        }

        public void StopMoving(string direction)
        {
            Move(direction, false);
        }

        public void Move(string direction, bool startMoving)
        {
            if (LinearDirections.Valid(direction))
            {
                switch (direction)
                {
                    case "Left":
                        Moving.Left = startMoving;
                        _velocityUpdater(this);
                        break;
                    case "Right":
                        Moving.Right = startMoving;
                        break;
                    case "Up":
                        Moving.Up = startMoving;
                        break;
                    case "Down":
                        Moving.Down = startMoving;
                        break;
                }

                _velocityUpdater(this);
                UpdateRotation();
                OnMove(new MoveEvent(direction, startMoving));
            }
            else
            {
                throw new InvalidOperationException(direction + " is an unknown direction.");
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (!this.Frozen)
            {
                Position += Velocity * gameTime.Elapsed.TotalSeconds;
                base.Update(gameTime);
            }
        }

        private void UpdateRotation()
        {
            if (_rotateWithMovements && !Velocity.IsZero())
            {
                Rotation = Math.Atan2(Velocity.Y, Velocity.X);
            }
        }

        private static void UpdateVelocityNoMultiDirection(LinearMovementController movementController)
        {
            var velocity = Vector2d.Zero;

            if (velocity.IsZero())
            {
                if (movementController.Moving.Up)
                {
                    velocity.Y -= movementController.MoveSpeed;
                }
                if (movementController.Moving.Down)
                {
                    velocity.Y += movementController.MoveSpeed;
                }

                if (velocity.Y == 0)
                {
                    if (movementController.Moving.Left)
                    {
                        velocity.X -= movementController.MoveSpeed;
                    }
                    if (movementController.Moving.Right)
                    {
                        velocity.X += movementController.MoveSpeed;
                    }
                }
            }

            movementController.Velocity = velocity;
        }

        private void UpdateVelocityWithMultiDirection(LinearMovementController movementController)
        {
            var velocity = Vector2d.Zero;

            if (movementController.Moving.Up)
            {
                velocity.Y -= movementController.MoveSpeed;
            }
            if (movementController.Moving.Down)
            {
                velocity.Y += movementController.MoveSpeed;
            }
            if (movementController.Moving.Left)
            {
                velocity.X -= movementController.MoveSpeed;
            }
            if (movementController.Moving.Right)
            {
                velocity.X += movementController.MoveSpeed;
            }

            this.Velocity = velocity;
        }
    }
}
