using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace WheelSpinGame
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private WheelGameConfig _wheelGameConfig;
        [SerializeField] private WheelView _wheelView;
        [SerializeField] private RewardView _rewardView;
        private ZoneManager _zoneManager;
        private WheelController _wheelController;
        

        private void Start()
        {
            _zoneManager = new ZoneManager(_wheelGameConfig.wheels[0]);
            _wheelController = new WheelController();

            _wheelView.DisplayWheel(_zoneManager.CurrWheelData);
        }

        public void OnSpinButtonClicked()
        {
            WheelData currWheelData = _zoneManager.CurrWheelData;
            WheelSliceData resultSlice = _wheelController.SpinWheel(currWheelData, out int sliceIndex);

            StartCoroutine(HandleSpinCoroutine(resultSlice, sliceIndex));
        }

        private IEnumerator HandleSpinCoroutine(WheelSliceData resultSlice, int sliceIndex)
        {
            yield return _wheelView.AnimateWheelSpin(sliceIndex).WaitForCompletion();

            if (resultSlice.SliceType == SliceType.Reward)
            {
                yield return _rewardView.DisplayReward(resultSlice).WaitForCompletion();
                //TODO: ZONE MANAGER GET NEXT ZONE WHEEL DATA SOMEWHERE????
                _wheelView.DisplayWheel(_wheelGameConfig.wheels[1]); //FIXME: FOR TESTING //(_zoneManager.CurrWheelData);
                yield return StartCoroutine(_rewardView.MoveRewardCoroutine());
            }
        }
    }
}