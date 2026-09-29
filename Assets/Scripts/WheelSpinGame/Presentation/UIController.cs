using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WheelSpinGame
{
    [RequireComponent(typeof(GameManager))]
    public class UIController : MonoBehaviour
    {
        [Header("Views")]
        [SerializeField] private WheelView _wheelView;
        [SerializeField] private RewardView _rewardView;
        [SerializeField] private DeathView _deathView;
        [SerializeField] private PrizesView _prizesView;
        [SerializeField] private ExitView _exitView;
        [Header("Buttons")]
        [SerializeField] private Button _spinButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _giveUpButton;
        [SerializeField] private Button _reviveButton;
        [SerializeField] private Button _goBackButton;
        [SerializeField] private Button _collectRewardsButton;

        public void Initialize(GameManager gameManager, WheelData wheelData)
        {
            _wheelView.DisplayWheel(wheelData);

            _spinButton.onClick.AddListener(gameManager.OnSpinButtonClicked);
            _exitButton.onClick.AddListener(gameManager.OnExitButtonClicked);
            _giveUpButton.onClick.AddListener(gameManager.OnGiveUpButtonClicked);
            _reviveButton.onClick.AddListener(gameManager.OnReviveButtonClicked);
            _goBackButton.onClick.AddListener(gameManager.OnGoBackButtonClicked);
            _collectRewardsButton.onClick.AddListener(gameManager.OnCollectRewardsButtonClicked);
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

        public void DisplayDeathPanel(bool val)
        {
            _deathView.DisplayDeathPanel(val);
        }
        public void DisplayExitPanel(bool val)
        {
            _exitView.DisplayExitPanel(val);
        }

        public void DisplayPrizes(List<PrizeData> prizes)
        {
            _prizesView.DisplayPrizes(prizes);
        }

        public void ClearPrizes()
        {
            _prizesView.ClearPrizes();
        }

        public void SetSpinButtonInteractable(bool val)
        {
            _spinButton.interactable = val;
        }
        public void SetExitButtonInteractable(bool val)
        {
            _exitButton.interactable = val;
        }
    }
}