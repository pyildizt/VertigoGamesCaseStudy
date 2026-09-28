using System;
using UnityEngine;

namespace WheelSpinGame
{
    public enum SliceType
    {
        Reward,
        Bomb
    }

    [Serializable]
    public class WheelSliceData
    {
        [SerializeField] private SliceType _sliceType;
        [SerializeField] private Sprite _iconSprite;
        [SerializeField] private int _count;

        public SliceType SliceType { get { return _sliceType; } }
        public Sprite IconSprite { get { return _iconSprite; } }
        public int Count { get { return _count; } }

        public WheelSliceData(SliceType sliceType, Sprite iconSprite, int count)
        {
            _sliceType = sliceType;
            _iconSprite = iconSprite;
            _count = count;
        }
    }
}