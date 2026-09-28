using System.Collections.Generic;

namespace WheelSpinGame
{
    public class ZoneManager
    {
        private readonly List<WheelData> _wheels;
        public int CurrZoneNumber { get; private set; }

        public ZoneManager(List<WheelData> wheels) 
        {
            CurrZoneNumber = 1;
            _wheels = wheels;
        }

        public void MoveToNextZone()
        {
            if (CurrZoneNumber < _wheels.Count)
            {
                CurrZoneNumber++;
            }
        }

        public WheelData GetCurrWheel()
        {
            return _wheels[CurrZoneNumber - 1];
        }

        public void Reset()
        {
            CurrZoneNumber = 1;
        }
    }
}