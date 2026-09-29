using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WheelSpinGame
{
    [System.Serializable]
    public struct WheelVisuals
    {
        public Sprite bronzeSpinBase;
        public Sprite bronzeSpinIndicator;
        public Color bronzeColor;
        public Sprite silverSpinBase;
        public Sprite silverSpinIndicator;
        public Color silverColor;
        public Sprite goldSpinBase;
        public Sprite goldSpinIndicator;
        public Color goldColor;
    }

    public class WheelView : MonoBehaviour
    {
        [SerializeField] private RectTransform _wholeWheelRectTransform;
        [SerializeField] private RectTransform _baseRectTransform;
        [Header("Wheel Visuals")]
        [SerializeField] private WheelVisuals _wheelVisuals;
        [SerializeField] private Image _spinBase;
        [SerializeField] private Image _spinIndicator;
        [SerializeField] private TMP_Text _spinTitle;
        [SerializeField] private TMP_Text _spinInfo;

        [SerializeField] private WheelSliceView[] _sliceViews;
        [Header("Wheel Spin Animation")]
        [SerializeField] private AnimationCurve wheelSpinCurve;

        public WheelSliceView[] SliceViews { get { return _sliceViews; } }

        private readonly bool _enableRandomOffset = true;

        private void DisplayWheelType(ZoneType zoneType)
        {
            switch (zoneType)
            {
                case ZoneType.Normal:
                    _spinBase.sprite = _wheelVisuals.bronzeSpinBase;
                    _spinIndicator.sprite = _wheelVisuals.bronzeSpinIndicator;
                    _spinTitle.text = "BRONZE SPIN";
                    _spinTitle.color = _wheelVisuals.bronzeColor;
                    _spinInfo.text = "";
                    break;
                case ZoneType.Safe:
                    _spinBase.sprite = _wheelVisuals.silverSpinBase;
                    _spinIndicator.sprite = _wheelVisuals.silverSpinIndicator;
                    _spinTitle.text = "SILVER SPIN";
                    _spinTitle.color = _wheelVisuals.silverColor;
                    _spinInfo.text = "Safe Spin";
                    _spinInfo.color = _wheelVisuals.silverColor;
                    break;
                case ZoneType.Super:
                    _spinBase.sprite = _wheelVisuals.goldSpinBase;
                    _spinIndicator.sprite = _wheelVisuals.goldSpinIndicator;
                    _spinTitle.text = "GOLDEN SPIN";
                    _spinTitle.color = _wheelVisuals.goldColor;
                    _spinInfo.text = "Up To x10 Rewards";
                    _spinInfo.color = _wheelVisuals.goldColor;
                    break;
            }
        }

        public void DisplayWheel(WheelData wheelData)
        {
            DisplayWheelType(wheelData.ZoneType);
            for (int i = 0; i < _sliceViews.Length; i++)
            {
                _sliceViews[i].SetData(wheelData.WheelSlices[i]);
            }
            ResetWheelRotation();
        }

        public Tween AnimateWheelSpin(int sliceIndex)
        {
            int turnCount = 3;
            float turnDurationSeconds = 2f;

            // Reset wheel angle
            ResetWheelRotation();

            float totalTurns = turnCount + (sliceIndex / (float)WheelData.wheelSliceCount);
            float sliceAngle = sliceIndex * (360f / WheelData.wheelSliceCount);

            // Calculate total rotation and duration
            float totalRotation = 360f * turnCount + sliceAngle;
            float totalDuration = turnDurationSeconds * totalTurns;

            // If enabled, add random offset to make it look more natural
            if (_enableRandomOffset)
            {
                float maxRandomOffset = 15f;
                totalRotation += Random.Range(-maxRandomOffset, maxRandomOffset);
            }

            return _baseRectTransform.DORotate(new Vector3(0, 0, totalRotation), totalDuration, RotateMode.FastBeyond360).SetEase(wheelSpinCurve); //.SetEase(Ease.InOutSine);
        }

        public void ResetWheelRotation()
        {
            _baseRectTransform.localRotation = Quaternion.identity;
        }

        public IEnumerator DisplayWheelWithAnimation(WheelData wheelData)
        {
            float duration = .4f;
            yield return _baseRectTransform.DOScale(0f, duration).SetEase(Ease.Flash).WaitForCompletion();
            DisplayWheelType(wheelData.ZoneType);
            for (int i = 0; i < _sliceViews.Length; i++)
            {
                _sliceViews[i].SetData(wheelData.WheelSlices[i]);
            }
            ResetWheelRotation();
            yield return _baseRectTransform.DOScale(1f, duration).SetEase(Ease.Flash).WaitForCompletion();
        }
    }
}