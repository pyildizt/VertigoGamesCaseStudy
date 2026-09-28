using System;
using UnityEngine;

namespace WheelSpinGame
{
    public enum ZoneType
    {
        Normal,
        Safe,
        Super
    }

    [Serializable]
    public class WheelData
    {
        public static readonly int wheelSliceCount = 8;
        [SerializeField] private int _zoneNumber;
        [SerializeField] private ZoneType _zoneType;
        [SerializeField] private WheelSliceData[] _wheelSlices = new WheelSliceData[wheelSliceCount];

        public int ZoneNumber { get { return _zoneNumber; } }
        public ZoneType ZoneType { get { return _zoneType; } }
        public WheelSliceData[] WheelSlices { get { return _wheelSlices; } }

        public WheelData(int zoneNumber, ZoneType zoneType)
        {
            _zoneNumber = zoneNumber;
            _zoneType = zoneType;
            _wheelSlices = new WheelSliceData[wheelSliceCount];
        }

        public void SetWheelSlice(int index, WheelSliceData slice)
        {
            _wheelSlices[index] = slice;
        }

        /// <summary>
        /// Make sure there are always _wheelSliceCount number of slices.
        /// </summary>
        public void ValidateWheelSlices()
        {
            if (_wheelSlices == null || _wheelSlices.Length != wheelSliceCount)
            {
                Array.Resize(ref _wheelSlices, wheelSliceCount);
            }
        }
    }
}