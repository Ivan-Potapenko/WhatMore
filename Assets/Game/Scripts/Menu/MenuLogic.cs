using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class MenuLogic : MonoBehaviour {

        [SerializeField]
        private Button _playButton;

        [SerializeField]
        private Button _leaderboardButton;

        [SerializeField]
        private MenuScreenManager _menuScreenManager;

        private void Awake() {
            _playButton.onClick.AddListener(_menuScreenManager.ActivatePresetSelectScreen);
            _leaderboardButton.onClick.AddListener(_menuScreenManager.ActivateLeaderboardScreen);
        }
    }
}