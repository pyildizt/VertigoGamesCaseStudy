using UnityEngine;
using UnityEngine.UI;

namespace WheelSpinGame
{
    [RequireComponent(typeof(GameManager))]
    public class UIController : MonoBehaviour
    {
        [SerializeField] private Button _spinButton;
        private GameManager _gameManager;

        private void Awake()
        {
            _gameManager = GetComponent<GameManager>();
        }

        private void Start()
        {
            _spinButton.onClick.AddListener(_gameManager.OnSpinButtonClicked);
        }
    }
}