using UnityEngine;

namespace WheelSpinGame
{
    public class ExitView : MonoBehaviour
    {
        [SerializeField] private RectTransform _exitRootTransform;

        private void Start()
        {
            _exitRootTransform.gameObject.SetActive(false);
        }

        public void DisplayExitPanel(bool val)
        {
            _exitRootTransform.gameObject.SetActive(val);
        }
    }
}
