using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WheelSpinGame
{
    public class Test : MonoBehaviour
    {
        [ContextMenuItem("Test", nameof(TestFunction))]
        public int test;

        [SerializeField] PrizesView prizesView;
        [SerializeField] GameManager gameManager;

        public void TestFunction()
        {
            for (int i = 0; i < 60; i++)
            {
                WheelData wheel = gameManager.WheelController.Wheels[i];
                foreach (WheelSliceData wheelSlice in wheel.WheelSlices)
                {
                    if (wheelSlice.SliceType == SliceType.Reward)
                    {
                        gameManager.PrizeManager.AddPrize(wheelSlice);
                        prizesView.DisplayPrizes(gameManager.PrizeManager.Prizes);
                    }
                }

            }           
        }
    }
}
