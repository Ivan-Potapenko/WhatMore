using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

namespace Game {

    public class LoseScreen : MonoBehaviour {

        [SerializeField]
        private Button _restartButton;

        [SerializeField]
        private Button _menuButton;

        [SerializeField]
        private Button _continueButton;

        [SerializeField]
        private TextMeshProUGUI _bestScoreLabel;

        public event Action _onContinueButtonClicked;

        private void Awake() {
            _restartButton.onClick.AddListener(RestartButtonClicked);
            _menuButton.onClick.AddListener(MenuButtonClicked);
            _continueButton.onClick.AddListener(ContinueButtonClicked);
        }

        public void Show(int score) {
            gameObject.SetActive(true);
            if(Account.Instance.BestScore < score) {
                Account.Instance.BestScore = score;
            }
            _bestScoreLabel.text = string.Format(_bestScoreLabel.text, score, Account.Instance.BestScore);
        }

        public void Hide() {
            gameObject.SetActive(false);
        }

        private void ContinueButtonClicked() {
            YG2.RewardedAdvShow("Continue", () => {
                _onContinueButtonClicked?.Invoke();
            });
        }

        private void MenuButtonClicked() {
            SceneManager.LoadScene("MenuScene");
        }

        private void RestartButtonClicked() {
            SceneManager.LoadScene("GameScene");
        }
    }
}
