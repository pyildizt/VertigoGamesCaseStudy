using System.Collections.Generic;
using UnityEngine;

namespace WheelSpinGame
{
    [CreateAssetMenu(fileName = "WheelGameConfig", menuName = "Wheel Spin Game/Wheel Game Config")]
    public class WheelGameConfig : ScriptableObject
    {
        [SerializeField] private List<Sprite> rewardSprites = new List<Sprite>();
        public List<WheelData> wheels = new List<WheelData>(60);

        private void OnValidate()
        {
            foreach (WheelData wheel in  wheels)
            {
                if (wheel != null)
                {
                    wheel.ValidateWheelSlices();
                }
            }
        }
    }
}