using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WheelSpinGame
{
    public class PrizesView : MonoBehaviour
    {
        [SerializeField] private Transform prizeParent;
        [SerializeField] private GameObject prizePrefab;

        public void PutRewardInPrizes(WheelSliceData wheelSliceData)
        {
            GameObject newPrize = Instantiate(prizePrefab);
            newPrize.GetComponent<Image>().sprite = wheelSliceData.IconSprite;
            newPrize.GetComponentInChildren<TMP_Text>().text = wheelSliceData.Count.ToString();
            newPrize.transform.SetParent(prizeParent);
        }
    }
}
