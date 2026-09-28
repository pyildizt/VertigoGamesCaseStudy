using DG.Tweening;
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

        private bool _isWheelSpinning;

        private void Awake()
        {
            _uiController = GetComponent<UIController>();
        }

        private void Start()
        {
            _wheelController = new WheelController(_wheelGameConfig);
            _zoneManager = new ZoneManager(_wheelController.Wheels);
            _prizeManager = new PrizeManager();

            _uiController.Initialize(this, _zoneManager.GetCurrWheel());

            _isWheelSpinning = false;
        }

        public void OnSpinButtonClicked()
        {
            WheelData currWheel = _zoneManager.GetCurrWheel();
            WheelSliceData resultSlice = _wheelController.SpinWheel(currWheel, out int sliceIndex);

            StartCoroutine(HandleSpinCoroutine(resultSlice, sliceIndex));
        }

        private IEnumerator HandleSpinCoroutine(WheelSliceData resultSlice, int sliceIndex)
        {
            _isWheelSpinning = true;
            _uiController.SetSpinButtonInteractable(false);

            yield return _uiController.AnimateWheelSpin(sliceIndex);

            if (resultSlice.SliceType == SliceType.Reward)
            {
                yield return _uiController.DisplayReward(resultSlice);
                
                // In between displaying reward and moving it, swiftly display the wheel for the next zone
                _zoneManager.MoveToNextZone();
                if (_zoneManager.CurrZoneNumber == _wheelGameConfig.NumberOfZones)
                { 
                    // TODO: END GAME!
                    yield break;
                }
                _uiController.DisplayWheel(_zoneManager.GetCurrWheel());

                yield return _uiController.MoveReward();
                _uiController.PutRewardInPrizes(resultSlice);
            }
            else
            {
                _uiController.DisplayDeathPanel();
            }
            _uiController.SetSpinButtonInteractable(true);
        }
    }
}