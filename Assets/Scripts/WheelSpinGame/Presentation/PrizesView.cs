using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WheelSpinGame
{
    public class PrizesView : MonoBehaviour
    {
        [SerializeField] private Transform _prizeParent;
        [SerializeField] private GameObject _prizePrefab;

        public void DisplayPrizes(List<PrizeData> prizes)
        {
            ClearPrizes();

            foreach (PrizeData prize in prizes)
            {
                GameObject newPrize = Instantiate(_prizePrefab, _prizeParent);

                newPrize.GetComponentInChildren<Image>().sprite = prize.IconSprite;
                newPrize.GetComponentInChildren<TMP_Text>().text = prize.Count.ToString();
            }
        }
        public void ClearPrizes()
        {
            foreach (Transform child in _prizeParent)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
