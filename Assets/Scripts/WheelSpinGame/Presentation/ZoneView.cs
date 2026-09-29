using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WheelSpinGame
{
    public class ZoneView : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private RectTransform _imageRectTransform;
        [SerializeField] private Image _zoneImage;
        [SerializeField] private TMP_Text _zoneNumberText;

        public RectTransform RectTransform {  get { return _rectTransform; } }
        public RectTransform ImageRectTransform { get { return _imageRectTransform; } }

        public void SetZone(int zoneNumber, Sprite zoneSprite, Color color)
        {
            _zoneNumberText.text = zoneNumber.ToString();
            _zoneImage.sprite = zoneSprite;
            _zoneImage.color = color;
        }

        public void SetScale(float scale)
        {
            _imageRectTransform.localScale = Vector3.one * scale;
        }

        public void SetSprite(Sprite zoneSprite)
        {
            _zoneImage.sprite = zoneSprite;
        }

        public void SetColor(Color color)
        {
            _zoneImage.color = color;
        }
    }
}
