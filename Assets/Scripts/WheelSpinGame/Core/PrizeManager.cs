using System.Collections.Generic;
using UnityEngine;

namespace WheelSpinGame
{
    public class PrizeData
    {
        public Sprite IconSprite { get; }
        public int Count { get; private set; }

        public PrizeData(Sprite iconSprite, int count)
        {
            IconSprite = iconSprite;
            Count = count;
        }

        public void AddCount(int count)
        {
            Count += count;
        }
    }

    public class PrizeManager
    {
        private readonly List<PrizeData> _prizes;
        public List<PrizeData> Prizes { get { return _prizes; } }

        public PrizeManager()
        {
            _prizes = new List<PrizeData>();
        }

        public void AddPrize(WheelSliceData wheelSlice)
        {
            if (wheelSlice == null)
                return;

            PrizeData existingPrize = _prizes.Find(prize => prize.IconSprite == wheelSlice.IconSprite);
            if (existingPrize != null)
            {
                existingPrize.AddCount(wheelSlice.Count);
                return;
            }

            _prizes.Add(new PrizeData(wheelSlice.IconSprite, wheelSlice.Count));
        }

        public void ClearPrizes()
        {
            _prizes.Clear();
        }
    }
}
