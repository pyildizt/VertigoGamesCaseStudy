using System;
using UnityEngine;

namespace WheelSpinGame
{
    [Serializable]
    public class WheelSliceData
    {
        [SerializeField] private WheelSlice wheelSlice;
        [SerializeField] private int count;

        public SliceType SliceType { get { return wheelSlice.sliceType; } }
        public Sprite IconSprite { get { return wheelSlice.iconSprite; } }
        public int Count { get { return count; } }
    }
}