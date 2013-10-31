using System;

namespace EndGate.Server
{
    /// <summary>
    /// Defines a GameConfiguration object that is used to represent the current state of a game object.
    /// </summary>
    public class GameConfiguration
    {
        private const int DefaultUpdateRate = 40;
        private const int DefaultPushRate = 25;

        private Action<int> _updateRateSetter;
        private Action<int> _pushRateSetter;
        private int _savedUpdateRate;
        private int _savedPushRate;

        /// <summary>
        /// Creates a new instance of the GameConfiguration object.
        /// </summary>
        /// <param name="registration">An object that provides functions to modify the update and push rates.</param>
        public GameConfiguration(GameRegistration registration)
        {
            _updateRateSetter = registration.UpdateRateSetter;
            _pushRateSetter = registration.PushRateSetter;

            UpdateRate = DefaultUpdateRate;
            PushRate = DefaultPushRate;
        }

        /// <summary>
        /// Gets or sets the UpdateRate of the game.  Update rates are represented as X many updates per second.
        /// </summary>
        public int UpdateRate
        {
            get
            {
                return _savedUpdateRate;
            }
            set
            {
                _savedUpdateRate = value;
                _updateRateSetter(_savedUpdateRate);
            }
        }

        /// <summary>
        /// Gets or sets the PushRate of the game.  Push rates are represented as X many pushes per second.
        /// </summary>
        public int PushRate
        {
            get
            {
                return _savedPushRate;
            }
            set
            {
                _savedPushRate = value;
                _pushRateSetter(_savedPushRate);
            }
        }
    }
}
