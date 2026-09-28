using System.Collections.Generic;

namespace WheelSpinGame
{
    public class PrizeManager
    {
        private List<WheelSliceData> _prizes;
        public List<WheelSliceData> Prizes {  get { return _prizes; } }

        public PrizeManager()
        {
            _prizes = new List<WheelSliceData>();
        }

        public void AddPrize(WheelSliceData prize)
        {
            if (prize != null)
            {
                _prizes.Add(prize);
            }
        }

        public void ClearPrizes()
        {
            _prizes.Clear();
        }
    }
}
