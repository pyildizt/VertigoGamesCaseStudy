using UnityEngine;

namespace WheelSpinGame
{
    public enum SliceType
    {
        Reward,
        Bomb
    }

    [CreateAssetMenu(fileName = "WheelSlice", menuName = "Wheel Spin Game/WheelSlice")]
    public class WheelSlice : ScriptableObject
    {
        public SliceType sliceType;
        public Sprite iconSprite;
    }
}