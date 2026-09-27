using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WheelSpinGame
{
    public class WheelSliceView : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text count;
        private readonly float bombScale = 2f;
        private readonly Vector3 rewardPosition = new Vector3(0f, 10f, 0f);

        public void SetData(WheelSliceData data)
        {
            if (data.SliceType == SliceType.Reward)
            {
                icon.rectTransform.localPosition = rewardPosition;
                icon.rectTransform.localScale = Vector3.one;

                icon.sprite = data.IconSprite;
                count.text = 'x' + data.Count.ToString();
            }
            else
            {
                icon.rectTransform.localPosition = Vector3.zero;
                icon.rectTransform.localScale = Vector3.one * bombScale;

                icon.sprite = data.IconSprite;
                count.text = "";
            }            
        }

        public Sprite GetIconSprite()
        {
            return icon.sprite;
        }

        public string GetCountString()
        {
            return 'x' + count.text;
        }
    }
}