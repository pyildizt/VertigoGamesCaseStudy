using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace WheelSpinGame
{
    [RequireComponent(typeof(GameManager))]
    public class UIController : MonoBehaviour
    {
        [SerializeField] private WheelView _wheelView;
        [SerializeField] private RewardView _rewardView;
        [SerializeField] private DeathView _deathView;
        [SerializeField] private PrizesView _prizesView;
        [SerializeField] private Button _spinButton;

        public void Initialize(GameManager gameManager, WheelData wheelData)
        {
            _wheelView.DisplayWheel(wheelData);

            _spinButton.onClick.AddListener(gameManager.OnSpinButtonClicked);
        }

        public IEnumerator AnimateWheelSpin(int sliceIndex)
        {
            yield return _wheelView.AnimateWheelSpin(sliceIndex).WaitForCompletion();
        }

        public IEnumerator DisplayReward(WheelSliceData resultSlice)
        {
            yield return _rewardView.DisplayReward(resultSlice).WaitForCompletion();
        }

        public void DisplayWheel(WheelData wheelData)
        {
            _wheelView.DisplayWheel(wheelData);
        }

        public IEnumerator MoveReward()
        {
            yield return _rewardView.MoveReward();
            _rewardView.SetActive(false);
        }

        public void DisplayDeathPanel()
        {
            _deathView.DisplayDeathPanel();
        }

        public void PutRewardInPrizes(WheelSliceData wheelSliceData)
        {
            _prizesView.PutRewardInPrizes(wheelSliceData);
        }

        public void SetSpinButtonInteractable(bool val)
        {
            _spinButton.interactable = val;
        }
    }
}