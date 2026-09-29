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
        [SerializeField] private ZoneListView _zoneListView;
        [SerializeField] private CollectedRewardsView _collectedRewardsView;
        [Header("Buttons")]
        [SerializeField] private Button _spinButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _giveUpButton;
        [SerializeField] private Button _reviveButton;
        [SerializeField] private Button _goBackButton;
        [SerializeField] private Button _collectRewardsButton;
        [SerializeField] private Button _playAgainButton;

        public void Initialize(GameManager gameManager, WheelData wheelData, int numberOfZones)
        {
            _wheelView.DisplayWheel(wheelData);
            _zoneListView.Initialize(numberOfZones);

            _spinButton.onClick.AddListener(gameManager.OnSpinButtonClicked);
            _exitButton.onClick.AddListener(gameManager.OnExitButtonClicked);
            _giveUpButton.onClick.AddListener(gameManager.OnGiveUpButtonClicked);
            _reviveButton.onClick.AddListener(gameManager.OnReviveButtonClicked);
            _goBackButton.onClick.AddListener(gameManager.OnGoBackButtonClicked);
            _collectRewardsButton.onClick.AddListener(gameManager.OnCollectRewardsButtonClicked);
            _playAgainButton.onClick.AddListener(gameManager.OnPlayAgainButtonClicked);
        }

        private void OnValidate()
        {
            Button[] buttons = GetComponentsInChildren<Button>(true);
            foreach (Button button in buttons)
            {
                switch (button.name)
                {
                    case "ui_button_spin":
                        _spinButton = button;
                        break;

                    case "ui_button_exit":
                        _exitButton = button;
                        break;

                    case "ui_button_give_up":
                        _giveUpButton = button;
                        break;

                    case "ui_button_revive":
                        _reviveButton = button;
                        break;

                    case "ui_button_go_back":
                        _goBackButton = button;
                        break;

                    case "ui_button_collect_rewards":
                        _collectRewardsButton = button;
                        break;

                    case "ui_button_play_again":
                        _playAgainButton = button;
                        break;
                }
            }
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

        public IEnumerator DisplayWheelWithAnimation(WheelData wheelData)
        {
            yield return _wheelView.DisplayWheelWithAnimation(wheelData);
        }

        public IEnumerator MoveReward()
        {
            yield return _rewardView.MoveReward().WaitForCompletion();
            _rewardView.SetActive(false);
        }

        public IEnumerator MoveToZone(int zoneIndex)
        {
            yield return _zoneListView.MoveToZone(zoneIndex);
        }

        public void DisplayDeathPanel(bool val)
        {
            _deathView.DisplayDeathPanel(val);
        }

        public void DisplayExitPanel(bool val)
        {
            _exitView.DisplayExitPanel(val);
        }

        public void DisplayCollectedRewardsPanel(bool val)
        {
            _collectedRewardsView.DisplayCollectedRewardsPanel(val);
        }

        public void DisplayCollectedRewards(List<PrizeData> prizes)
        {
            _collectedRewardsView.DisplayRewards(prizes);
        }

        public void DisplayPrizes(List<PrizeData> prizes)
        {
            _prizesView.DisplayPrizes(prizes);
        }

        public void ClearPrizes()
        {
            _prizesView.ClearPrizes();
        }

        public void ResetZoneList(int numberOfZones)
        {
            _zoneListView.Initialize(numberOfZones);
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