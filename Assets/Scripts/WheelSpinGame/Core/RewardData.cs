using UnityEngine;

namespace WheelSpinGame
{
    public enum RewardCategory
    {
        Currency,
        Chest,
        IconPoint,
        Accessory,
        Weapon,
        Consumable

    }

    public class RewardData : ScriptableObject
    {
        [SerializeField] private Sprite _iconSprite;
        [SerializeField] private RewardCategory _rewardCategory;
        [SerializeField] private int _rewardTier; // 1,2,3
        [SerializeField] private int _minCount;
        [SerializeField] private int _maxCount;

        public Sprite IconSprite { get { return _iconSprite; } }
        public RewardCategory RewardCategory { get { return _rewardCategory; } }
        public int RewardTier { get { return _rewardTier; } }
        public int MinCount { get { return _minCount; } }
        public int MaxCount { get { return _maxCount; } }

        public void SetDefaultValues(Sprite iconSprite)
        {
            _iconSprite = iconSprite;
            _rewardCategory = RewardCategory.Weapon;
            _rewardTier = 1;
            _minCount = 1;
            _maxCount = 1;
        }

        public void OnValidate()
        {
            if ((_rewardTier < 1) || (_rewardTier > 3))
            {
                Debug.LogWarning("Reward Tier value " + _rewardTier + " should be 1, 2, or 3");
                _rewardTier = 1;
            }
            if (_minCount < 1)
            {
                _minCount = 1;
            }
            if (_maxCount < 1)
            {
                _maxCount = 1;
            }
            if (_minCount > _maxCount)
            {
                Debug.LogWarning("Min count value cannot be smaller than max count value");
                _maxCount = _minCount;
            }
        }
    }
}