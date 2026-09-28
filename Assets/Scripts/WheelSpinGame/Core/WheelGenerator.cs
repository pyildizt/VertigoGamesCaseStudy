using System.Collections.Generic;
using UnityEngine;

namespace WheelSpinGame
{
    public class WheelGenerator
    {
        private readonly WheelGameConfig _wheelGameConfig;

        private readonly int _lowTierEndZone;
        private List<RewardData> _lowTierRewards;
        private List<RewardData> _midTierRewards;
        private List<RewardData> _highTierRewards;

        public WheelGenerator(WheelGameConfig wheelGameConfig)
        {
            _wheelGameConfig = wheelGameConfig;

            _lowTierEndZone = _wheelGameConfig.NumberOfZones / 2;
            CreateTierRewardLists();
        }

        private void CreateTierRewardLists()
        {
            _lowTierRewards = new List<RewardData>();
            _midTierRewards = new List<RewardData>();
            _highTierRewards = new List<RewardData>();

            foreach (RewardData reward in _wheelGameConfig.RewardData)
            {
                switch (reward.RewardTier)
                {
                    case 1:
                        _lowTierRewards.Add(reward);
                        break;

                    case 2:
                        _midTierRewards.Add(reward);
                        break;

                    case 3:
                        _highTierRewards.Add(reward);
                        break;
                }
            }
            //Debug.Log($"low: {_lowTierRewards.Count}, mid: {_midTierRewards.Count}, high: {_highTierRewards.Count}");
        }

        public List<WheelData> GenerateWheels()
        {
            List<WheelData> wheels = new List<WheelData>(_wheelGameConfig.NumberOfZones);
            for (int zoneNumber = 1; zoneNumber < _wheelGameConfig.NumberOfZones + 1; zoneNumber++)
            {
                // If wheel created in config, use that
                WheelData wheel = GetWheelFromConfig(zoneNumber);
                if (wheel == null)
                {
                    wheel = GenerateWheel(zoneNumber);
                }
                wheels.Add(wheel);
            }
            return wheels;
        }

        private WheelData GetWheelFromConfig(int zoneNumber)
        {
            foreach (WheelData wheel in _wheelGameConfig.WheelOverrides)
            {
                if (wheel.ZoneNumber == zoneNumber)
                {
                    return wheel;
                }
            }
            return null;
        }

        private WheelData GenerateWheel(int zoneNumber)
        {
            ZoneType zoneType = GetZoneType(zoneNumber);
            List<RewardData> availableRewards = GetAvailableRewards(zoneNumber);

            WheelData newWheel = new WheelData(zoneNumber, zoneType);

            // If normal zone, add a bomb
            int wheelSliceIndex = 0;
            if (zoneType == ZoneType.Normal)
            {
                newWheel.SetWheelSlice(wheelSliceIndex, GenerateBombSlice());
                wheelSliceIndex++;
            }
            while (wheelSliceIndex < WheelData.wheelSliceCount)
            {
                newWheel.SetWheelSlice(wheelSliceIndex, GenerateRewardSlice(availableRewards));
                wheelSliceIndex++;
            }
            return newWheel;
        }

        private WheelSliceData GenerateBombSlice()
        {
            Sprite bombSprite = _wheelGameConfig.BombSprite;
            return new WheelSliceData(SliceType.Bomb, bombSprite, 0);
        }

        private WheelSliceData GenerateRewardSlice(List<RewardData> rewards)
        {
            // Get random reward
            RewardData reward = rewards[Random.Range(0, rewards.Count)];

            // Get random count
            int count = Random.Range(reward.MinCount, reward.MaxCount + 1);

            return new WheelSliceData(SliceType.Reward, reward.IconSprite, count);
        }

        private ZoneType GetZoneType(int zoneNumber)
        {
            if (zoneNumber % 30 == 0)
            {
                return ZoneType.Super;
            }
            if (zoneNumber % 5 == 0)
            {
                return ZoneType.Safe;
            }
            return ZoneType.Normal;
        }

        private List<RewardData> GetAvailableRewards(int zoneNumber)
        {
            // If super zone, use high tier rewards only
            if (zoneNumber % 30 == 0)
            {
                return _highTierRewards;
            }

            List<RewardData> availableRewards = new();
            if (zoneNumber <= _lowTierEndZone)
            {
                // Unlock low tier rewards based on zone progress
                float progress = (float)zoneNumber / _lowTierEndZone;

                int lowAmount = Mathf.CeilToInt(_lowTierRewards.Count * progress);
                for (int i = 0; i < lowAmount; i++)
                {
                    availableRewards.Add(_lowTierRewards[i]);
                }
            }
            else
            {
                // If all low tier rewards unlocked, move onto mid tier rewards
                availableRewards.AddRange(_lowTierRewards);

                float progress = (float)(zoneNumber - _lowTierEndZone) / (_wheelGameConfig.NumberOfZones - _lowTierEndZone);

                int midAmount = Mathf.CeilToInt(_midTierRewards.Count * progress);
                for (int i = 0; i < midAmount; i++)
                {
                    availableRewards.Add(_midTierRewards[i]);
                }
            }
            return availableRewards;
        }
    }
}