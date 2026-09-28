using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

namespace WheelSpinGame
{
    public class DeathView : MonoBehaviour
    {
        [SerializeField] private RectTransform _deathRootTransform;

        private void Start()
        {
            _deathRootTransform.gameObject.SetActive(false);
        }

        public void DisplayDeathPanel()
        {
            _deathRootTransform.gameObject.SetActive(true);
        }

    }
}