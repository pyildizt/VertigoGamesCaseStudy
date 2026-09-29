using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace WheelSpinGame
{
    [System.Serializable]
    public struct ZoneVisuals
    {
        public Sprite currZoneNormal;
        public Sprite nextZoneNormal;
        public Sprite safeZone;
        public Color superZoneColor;
        public Color prevZoneColor;
    }

    public class ZoneListView : MonoBehaviour
    {
        [SerializeField] private RectTransform _zoneList;
        [SerializeField] private RectTransform _viewport;
        [SerializeField] private ZoneView _zonePrefab;
        [SerializeField] private ZoneVisuals _zoneVisuals;

        private List<ZoneView> _zoneViews;
        private float _zoneStep;
        private int _currentZoneIndex;

        public void Initialize(int numberOfZones)
        {
            _zoneViews = new List<ZoneView>();

            ClearZones();

            float zoneSpacing = 50f;
            _zoneStep = _zonePrefab.RectTransform.rect.width + zoneSpacing;

            CreateZones(numberOfZones);

            _currentZoneIndex = 1;

            SetZoneScales();
        }

        private void CreateZones(int numberOfZones)
        {
            for (int zoneNumber = 1; zoneNumber < numberOfZones + 1; zoneNumber++)
            {
                ZoneView zoneView = Instantiate(_zonePrefab, _zoneList);
                if (zoneNumber == 1)
                {
                    zoneView.SetZone(zoneNumber, _zoneVisuals.currZoneNormal, Color.white);
                }
                else if (zoneNumber % 30 == 0)
                {
                    zoneView.SetZone(zoneNumber, _zoneVisuals.currZoneNormal, _zoneVisuals.superZoneColor);
                }
                else if (zoneNumber % 5 == 0)
                {
                    zoneView.SetZone(zoneNumber, _zoneVisuals.safeZone, Color.white);
                }
                else
                {
                    zoneView.SetZone(zoneNumber, _zoneVisuals.nextZoneNormal, Color.white);
                }
                _zoneViews.Add(zoneView);

                RectTransform zoneRectTransform = zoneView.RectTransform;
                zoneRectTransform.anchoredPosition = new Vector2((zoneNumber - 1) * _zoneStep, 0f);
            }
            _viewport.anchoredPosition = Vector2.zero;
        }

        public IEnumerator MoveToZone(int zoneIndex)
        {
            if (zoneIndex >= _zoneViews.Count)
            {
                yield break;
            }
            _currentZoneIndex = zoneIndex;

            SetZoneScales();

            float targetX = -(_zoneStep * (_currentZoneIndex - 1));
            float duration = 0.5f;

            yield return _zoneList.DOAnchorPosX(targetX, duration).SetEase(Ease.InOutCubic).WaitForCompletion();

            // Change sprite to current
            if (zoneIndex % 5 != 0)
            {
                _zoneViews[zoneIndex - 1].SetSprite(_zoneVisuals.currZoneNormal);
            }
            // If prev exists, change sprite and color accordingly
            if (0 < zoneIndex)
            {
                _zoneViews[zoneIndex - 2].SetColor(_zoneVisuals.prevZoneColor);
                if ((zoneIndex - 1) % 5 != 0)
                {
                    _zoneViews[zoneIndex - 2].SetSprite(_zoneVisuals.nextZoneNormal);
                }
            }
        }

        private void SetZoneScales()
        {
            float normalZoneScale = 1f;
            float currZoneScale = 1.15f;

            for (int zoneNumber = 1; zoneNumber < _zoneViews.Count - 1; zoneNumber++)
            {
                if (_currentZoneIndex == zoneNumber)
                {
                    _zoneViews[zoneNumber - 1].SetScale(currZoneScale);
                }
                else
                {
                    _zoneViews[zoneNumber - 1].SetScale(normalZoneScale);
                }
            }
        }

        private void ClearZones()
        {
            foreach (ZoneView zoneView in _zoneViews)
            {
                Destroy(zoneView.gameObject);
            }
            _zoneViews.Clear();
        }
    }
}
