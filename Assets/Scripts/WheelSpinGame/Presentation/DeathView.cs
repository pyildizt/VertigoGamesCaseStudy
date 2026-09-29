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

        public void DisplayDeathPanel(bool val)
        {
            _deathRootTransform.gameObject.SetActive(val);
        }
    }
}