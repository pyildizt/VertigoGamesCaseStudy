using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WheelSpinGame
{
    public class RewardView : MonoBehaviour
    {
        [SerializeField] private RectTransform _rewardRootTransform;
        [SerializeField] private Image _rewardImage;
        [SerializeField] private RectTransform _rewardRectTransform;
        [SerializeField] private RectTransform _effectRectTransform;
        [SerializeField] private TMP_Text _countText;

        private void Start()
        {
            _rewardRootTransform.gameObject.SetActive(false);
        }

        public Sequence DisplayReward(WheelSliceData sliceData)
        {
            _rewardRootTransform.gameObject.SetActive(true);
            _rewardRootTransform.anchoredPosition = Vector2.zero;
            _rewardRootTransform.localScale = Vector3.zero;

            _rewardImage.sprite = sliceData.IconSprite;
            _countText.text = 'x' + sliceData.Count.ToString();

            // Make reward pop out in the middle of panel
            float popDuration = 0.6f;
            Sequence popSequence = DOTween.Sequence();
            popSequence.Append(_rewardRootTransform.DOScale(Vector3.one, popDuration).SetEase(Ease.InOutBack));

            // Spin star flash in the back
            Vector3 rotateTo = _effectRectTransform.localEulerAngles + new Vector3(0, 0, 15f);
            popSequence.Join(_effectRectTransform.DORotate(rotateTo, 3f, RotateMode.FastBeyond360));
            return popSequence;
        }

        public Tween MoveReward()
        {
            return _rewardRootTransform.DOScale(Vector3.zero, 1f).SetEase(Ease.InOutBack);
        }

        public void SetActive(bool val)
        {
            _rewardRootTransform.gameObject.SetActive(val);
        }
    }
}