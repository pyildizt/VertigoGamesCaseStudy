using System.Collections.Generic;
using UnityEngine;

namespace WheelSpinGame
{
    [CreateAssetMenu(fileName = "WheelGameConfig", menuName = "Wheel Spin Game/Wheel Game Config")]
    public class WheelGameConfig : ScriptableObject
    {
        [SerializeField] private int _numberOfZones = 60;
        [SerializeField] private Sprite _bombSprite;
        [SerializeField] private List<Sprite> _rewardSprites;
        [SerializeField] private List<RewardData> _rewardData;
        [SerializeField] private List<WheelData> _wheelOverrides;
        [SerializeField] private List<WheelData> _generatedWheelPreview = new();

        public int NumberOfZones { get { return _numberOfZones; } }
        public Sprite BombSprite { get { return _bombSprite; } }
        public List<Sprite> RewardSprites { get { return _rewardSprites; } }
        public List<RewardData> RewardData { get { return _rewardData; } }
        public List<WheelData> WheelOverrides { get { return _wheelOverrides; } }

        private void OnValidate()
        {
            foreach (WheelData wheel in _wheelOverrides)
            {
                if (wheel != null)
                {
                    wheel.ValidateWheelSlices();
                }
            }
        }

        public void GenerateWheelPreview()
        {
            WheelGenerator generator = new WheelGenerator(this);

            _generatedWheelPreview.Clear();
            _generatedWheelPreview.AddRange(generator.GenerateWheels());
        }
    }
}