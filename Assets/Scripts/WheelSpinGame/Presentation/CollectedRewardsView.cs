using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WheelSpinGame
{
    public class CollectedRewardsView : MonoBehaviour
    {
        [SerializeField] private RectTransform _collectedRewardsRootTransform;
        [SerializeField] private Transform _prizeParent;
        [SerializeField] private GameObject _prizePrefab;

        private List<GameObject> _prizeObjects = new();

        private void Start()
        {
            _collectedRewardsRootTransform.gameObject.SetActive(false);
        }

        public void DisplayRewards(List<PrizeData> prizes)
        {
            ClearPrizes();

            gameObject.SetActive(true);

            foreach (PrizeData prize in prizes)
            {
                GameObject prizeObject = Instantiate(_prizePrefab, _prizeParent);

                prizeObject.GetComponentInChildren<Image>().sprite = prize.IconSprite;
                prizeObject.GetComponentInChildren<TMP_Text>().text = prize.Count.ToString();

                prizeObject.transform.localScale = Vector3.zero;

                _prizeObjects.Add(prizeObject);
            }
            StartCoroutine(AnimatePrizes());
        }

        private IEnumerator AnimatePrizes()
        {
            float popDuration = 0.25f;
            float popDelay = 0.08f;
            foreach (GameObject prizeObject in _prizeObjects)
            {
                prizeObject.transform.DOScale(Vector3.one, popDuration).SetEase(Ease.OutBack);
                yield return new WaitForSeconds(popDelay);
            }
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void ClearPrizes()
        {
            foreach (GameObject prizeObject in _prizeObjects)
            {
                Destroy(prizeObject);
            }
            _prizeObjects.Clear();
        }

        public void DisplayCollectedRewardsPanel(bool val)
        {
            _collectedRewardsRootTransform.gameObject.SetActive(val);
        }
    }
}
