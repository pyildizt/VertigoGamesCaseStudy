using System.Collections;
using UnityEngine;
namespace WheelSpinGame
{
    [RequireComponent(typeof(UIController))]
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private WheelGameConfig _wheelGameConfig;
        private UIController _uiController;
        private ZoneManager _zoneManager;
        private PrizeManager _prizeManager;
        private WheelController _wheelController;
        private bool _isProcessing;

        public PrizeManager PrizeManager => _prizeManager;
        public WheelController WheelController => _wheelController;

        private void Awake()
        {
            _uiController = GetComponent<UIController>();
        }

        private void Start()
        {
            _wheelController = new WheelController(_wheelGameConfig);
            _zoneManager = new ZoneManager(_wheelController.Wheels);
            _prizeManager = new PrizeManager();

            _uiController.Initialize(this, _zoneManager.GetCurrWheel(), _wheelGameConfig.NumberOfZones);

            _isProcessing = false;
        }

        public void OnSpinButtonClicked()
        {
            WheelData currWheel = _zoneManager.GetCurrWheel();
            WheelSliceData resultSlice = _wheelController.SpinWheel(currWheel, out int sliceIndex);

            StartCoroutine(HandleSpinCoroutine(resultSlice, sliceIndex));
        }

        private IEnumerator HandleSpinCoroutine(WheelSliceData resultSlice, int sliceIndex)
        {
            _isProcessing = true;

            _uiController.SetSpinButtonInteractable(false);
            _uiController.SetExitButtonInteractable(false);

            // Spin wheel
            yield return _uiController.AnimateWheelSpin(sliceIndex);

            if (resultSlice.SliceType == SliceType.Reward)
            {
                // Display reward and put in prize list
                yield return _uiController.DisplayReward(resultSlice);
                yield return _uiController.MoveReward();
                _prizeManager.AddPrize(resultSlice);
                _uiController.DisplayPrizes(_prizeManager.Prizes);

                // Move to next zone
                _zoneManager.MoveToNextZone();
                yield return _uiController.MoveToZone(_zoneManager.CurrZoneNumber);
                if (_zoneManager.CurrZoneNumber == _wheelGameConfig.NumberOfZones)
                {
                    // TODO: END GAME!
                    yield break;
                }

                // Display next wheel
                yield return _uiController.DisplayWheelWithAnimation(_zoneManager.GetCurrWheel());

                _uiController.SetSpinButtonInteractable(true);
                _uiController.SetExitButtonInteractable(true);
            }
            else
            {
                // If bomb, show death panel
                _uiController.DisplayDeathPanel(true);
                _uiController.SetExitButtonInteractable(false);
            }
        }

        public void OnExitButtonClicked()
        {
            _uiController.DisplayExitPanel(true);
        }

        public void OnGiveUpButtonClicked()
        {
            _prizeManager.ClearPrizes();
            _uiController.ClearPrizes();

            _zoneManager.Reset();
            _uiController.DisplayWheel(_zoneManager.GetCurrWheel());
            _uiController.DisplayDeathPanel(false);

            _uiController.SetSpinButtonInteractable(true);
            _uiController.SetExitButtonInteractable(true);
        }

        public void OnReviveButtonClicked()
        {
            _uiController.DisplayWheel(_zoneManager.GetCurrWheel());
            _uiController.DisplayDeathPanel(false);

            _uiController.SetSpinButtonInteractable(true);
            _uiController.SetExitButtonInteractable(true);
        }

        public void OnGoBackButtonClicked()
        {
            _uiController.DisplayExitPanel(false);
        }

        public void OnCollectRewardsButtonClicked()
        {
            _uiController.DisplayExitPanel(false);
            //TODO:
            Debug.Log("need code for collect rewards");
        }

    }
}