using System;

namespace EndGate.Server
{
    public class GameConfiguration
    {
        private const int DefaultUpdateRate = 40;
        private const int DefaultPushRate = 25;

        private Action<int> _updateRateSetter;
        private Action<int> _pushRateSetter;
        private int _savedUpdateRate;
        private int _savedPushRate;

        public GameConfiguration(GameRegistration registration)
        {
            _updateRateSetter = registration.UpdateRateSetter;
            _pushRateSetter = registration.PushRateSetter;

            UpdateRate = DefaultUpdateRate;
            PushRate = DefaultPushRate;
        }

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
