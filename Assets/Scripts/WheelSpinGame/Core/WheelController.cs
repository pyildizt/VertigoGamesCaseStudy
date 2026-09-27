using UnityEngine;

namespace WheelSpinGame
{
    public class WheelController
    {
        public WheelSliceData SpinWheel(WheelData wheelData, out int randomSliceIndex)
        {
            if (wheelData == null || wheelData.WheelSlices.Length == 0)
            {
                randomSliceIndex = -1;
                throw new System.Exception("Wheel slices are null or empty.");
            }

            randomSliceIndex = Random.Range(0, wheelData.WheelSlices.Length);

            return wheelData.WheelSlices[randomSliceIndex];
        }
    }
}