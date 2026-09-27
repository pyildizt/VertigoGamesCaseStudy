namespace WheelSpinGame
{
    public class ZoneManager
    {
        public int CurrZoneNumber { get; private set; }
        public WheelData CurrWheelData { get; private set; }
        public bool IsWheelSpinning { get; set; }

        public ZoneManager(WheelData initWheelData) 
        {
            CurrZoneNumber = 0;
            CurrWheelData = initWheelData;
            IsWheelSpinning = false;
        }
    }
}