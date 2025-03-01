using UnityEngine;

namespace UI {

    public class MenuScreenManager : MonoBehaviour {

        [SerializeField]
        private GameObject _menuScreen;

        [SerializeField]
        private GameObject _leaderboardScreen;

        [SerializeField]
        private GameObject _presetSelectScreen;

        [SerializeField]
        private GameObject _homeButton;

        private void Awake() {
            ActivateMenuScreen();
        }

        public void ActivateMenuScreen() {
            DisableScreens();
            _menuScreen.gameObject.SetActive(true);
        }

        public void ActivateLeaderboardScreen() {
            DisableScreens();
            _leaderboardScreen.SetActive(true);
            _homeButton.SetActive(true);
        }

        public void ActivatePresetSelectScreen() {
            DisableScreens();
            _presetSelectScreen.SetActive(true);
            _homeButton.SetActive(true);
        }

        private void DisableScreens() {
            _menuScreen.SetActive(false);
            _leaderboardScreen.SetActive(false);
            _presetSelectScreen.SetActive(false);
            _homeButton.SetActive(false);
        }
    }
}
