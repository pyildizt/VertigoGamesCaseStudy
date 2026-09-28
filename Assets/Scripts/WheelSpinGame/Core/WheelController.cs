using System.Collections.Generic;
using UnityEngine;

namespace WheelSpinGame
{
    public class WheelController
    {
        public List<WheelData> Wheels { get; }
        
        public WheelController(WheelGameConfig wheelGameConfig)
        {
            WheelGenerator wheelGenerator = new WheelGenerator(wheelGameConfig);
            Wheels = wheelGenerator.GenerateWheels();
        }

        public WheelSliceData SpinWheel(WheelData wheelData, out int randomSliceIndex)
        {
            if (wheelData == null || wheelData.WheelSlices == null|| wheelData.WheelSlices.Length == 0)
            {
                randomSliceIndex = -1;
                throw new System.Exception("Wheel slices are null or empty.");
            }

            randomSliceIndex = Random.Range(0, wheelData.WheelSlices.Length);
            Debug.Log($"Chosen: slice {randomSliceIndex}: {wheelData.WheelSlices[randomSliceIndex].Count}");
            return wheelData.WheelSlices[randomSliceIndex];
        }
    }
}