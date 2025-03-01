using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class HomeButton : MonoBehaviour {

        [SerializeField]
        private Button _button;

        [SerializeField]
        private MenuScreenManager _screenManager;

        private void Awake() {
            _button.onClick.AddListener(_screenManager.ActivateMenuScreen);
        }
    }
}
