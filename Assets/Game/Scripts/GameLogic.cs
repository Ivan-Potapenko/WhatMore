using Data;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Game {

    public class GameLogic : MonoBehaviour {

        [SerializeField]
        private ChooseButton _leftImageButton;

        [SerializeField]
        private ChooseButton _rightImageButton;

        [SerializeField]
        private SelectableItemsConfig _selectableItemsConfig;

        [SerializeField]
        private TextMeshProUGUI _scoreLabel;

        [SerializeField]
        private float _showProcessTime;
        [SerializeField]
        private float _timeBeforeSwitch;

        private bool _showingResult;

        private int _score;

        private void Start() {
            _score = 0;
            _leftImageButton.onClick += OnLeftButtonClicked;
            _rightImageButton.onClick += OnRightButtonClicked;
            SetPair();
        }

        public void SetPair() {
            _scoreLabel.text = _score.ToString();
            var buttonsData = _selectableItemsConfig.ChoseButtonsData;
            var firstIndex = Random.Range(0, buttonsData.Length);
            var secondIndex = Random.Range(0, buttonsData.Length);
            while (firstIndex == secondIndex) {
                secondIndex = Random.Range(0, buttonsData.Length);
            }
            _leftImageButton.SetData(buttonsData[firstIndex]);
            _rightImageButton.SetData(buttonsData[secondIndex]);
        }

        private void OnLeftButtonClicked() {
            ShowResult(_leftImageButton.Data.Value >= _rightImageButton.Data.Value);
        }

        private void OnRightButtonClicked() {
            ShowResult(_leftImageButton.Data.Value <= _rightImageButton.Data.Value);
        }

        private void ShowResult(bool win) {
            if (_showingResult) {
                return;
            }
            StartCoroutine(ShowResultCoroutine(win));
        }

        private IEnumerator ShowResultCoroutine(bool win) {
            _showingResult = true;
            var currentTime = 0f;
            while (currentTime < _showProcessTime) {
                var showPercent = Mathf.Min(currentTime / _showProcessTime, 1);
                _leftImageButton.ProcessValueShow(showPercent);
                _rightImageButton.ProcessValueShow(showPercent);
                yield return null;
                currentTime += Time.deltaTime;
            }

            _leftImageButton.ShowResult(rightAnswer: _leftImageButton.Data.Value >= _rightImageButton.Data.Value);
            _rightImageButton.ShowResult(rightAnswer: _rightImageButton.Data.Value >= _leftImageButton.Data.Value);

            yield return new WaitForSeconds(_timeBeforeSwitch);

            if (win) {
                HandleWin();
            } else {
                HandleLose();
            }
            _showingResult = false;
        }

        private void HandleLose() {

        }

        private void HandleWin() {
            _score++;
            SetPair();
        }
    }
}
