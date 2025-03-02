using Data;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game {

    public class GameLogic : MonoBehaviour {

        [SerializeField]
        private List<ComparisonItem> _comparisonItems;

        [SerializeField]
        private SelectableItemsConfig _selectableItemsConfig;

        [SerializeField]
        private TextMeshProUGUI _scoreLabel;

        [SerializeField]
        private float _moveSpeed;

        [SerializeField]
        private Button _smallerButton;

        [SerializeField]
        private Button _biggerButton;

        [SerializeField]
        private float _showProcessTime;
        [SerializeField]
        private float _timeBeforeSwitch;

        [SerializeField]
        private GameObject _buttonGroup;

        [SerializeField]
        private ResultIndicatorGroup _resultIndicatorGroup;

        [SerializeField]
        private LoseScreen _loseScreen;

        private List<Vector3> _startPositions = new List<Vector3>();

        private int _currentImageIndex;

        private float _imageHeight;

        private bool _showingResult;

        private int _score;

        private Queue<int> _reservedIndexes = new Queue<int>();

        private void Start() {
            if (Account.Instance.currentSelectableItemsConfig != null) {
                _selectableItemsConfig = Account.Instance.currentSelectableItemsConfig;
            }
            for (var i = 0; i < _comparisonItems.Count; i++) {
                _startPositions.Add(_comparisonItems[i].RectTransform.anchoredPosition);
                _comparisonItems[i].SetData(GetRandomData());
            }
            _comparisonItems[0].ProcessValueShow(1);
            _score = 0;
            _currentImageIndex = 0;
            _imageHeight = (_comparisonItems[0].RectTransform.anchoredPosition - _comparisonItems[1].RectTransform.anchoredPosition).magnitude;
            _smallerButton.onClick.AddListener(SmallerButtonClicked);
            _biggerButton.onClick.AddListener(BiggerButtonClicked);
            _resultIndicatorGroup.HideResult();
            _loseScreen.Hide();
            _loseScreen._onContinueButtonClicked += () => {
                _loseScreen.Hide();
                ScrollImages();
            };
        }

        private void SmallerButtonClicked() {
            ShowResult(_comparisonItems[GetOffsetImageIndex(0)].Data.Value >= _comparisonItems[GetOffsetImageIndex(1)].Data.Value);
        }

        private void BiggerButtonClicked() {
            ShowResult(_comparisonItems[GetOffsetImageIndex(0)].Data.Value <= _comparisonItems[GetOffsetImageIndex(1)].Data.Value);
        }

        private void ShowResult(bool win) {
            if (_showingResult) {
                return;
            }
            StartCoroutine(ShowResultCoroutine(win));
        }

        private IEnumerator ShowResultCoroutine(bool win) {
            _buttonGroup.gameObject.SetActive(false);
            _showingResult = true;
            var currentTime = 0f;
            while (currentTime < _showProcessTime) {
                var showPercent = Mathf.Min(currentTime / _showProcessTime, 1);
                _comparisonItems[GetOffsetImageIndex(1)].ProcessValueShow(showPercent);
                yield return null;
                currentTime += Time.deltaTime;
            }

            yield return new WaitForSeconds(_timeBeforeSwitch);

            _resultIndicatorGroup.ShowResult(win);
            if (win) {
                HandleWin();
            } else {
                HandleLose();
            }
            _showingResult = false;
        }

        private void HandleLose() {
            _loseScreen.Show(_score);
        }

        private void HandleWin() {
            _score++;
            ScrollImages();
        }

        private void ScrollImages() {
            StartCoroutine(ScrollImagesCoroutine());
        }

        private IEnumerator ScrollImagesCoroutine() {
            var offsetSum = 0f;
            while (offsetSum < _imageHeight) {
                float delta = _moveSpeed * Time.deltaTime;
                offsetSum += delta;

                foreach (var img in _comparisonItems) {
                    img.RectTransform.anchoredPosition += Vector2.up * delta;
                }

                yield return null;
            }

            _currentImageIndex++;

            for (var i = 0; i < _comparisonItems.Count; i++) {
                _comparisonItems[i].RectTransform.anchoredPosition = _startPositions[GetOffsetImageIndexPosition(i)];
            }

            _comparisonItems[GetOffsetImageIndex(2)].SetData(GetRandomData());
            _buttonGroup.gameObject.SetActive(true);
            _resultIndicatorGroup.HideResult();
            Account.Instance.Money++;
        }

        private int GetOffsetImageIndexPosition(int i) {
            var index = i - _currentImageIndex % _comparisonItems.Count;
            return index < 0 ? _comparisonItems.Count + index : index;
        }

        private int GetOffsetImageIndex(int i) {
            return (_currentImageIndex + i) % _comparisonItems.Count;
        }

        public ChoseButtonData GetRandomData() {
            if (_reservedIndexes.Count > 4) {
                _reservedIndexes.Dequeue();
            }
            _scoreLabel.text = _score.ToString();
            var buttonsData = _selectableItemsConfig.ChoseButtonsData;
            var freeIndexes = new List<int>();
            for (var i = 0; i < buttonsData.Length; i++) {
                if (_reservedIndexes.Contains(i)) {
                    continue;
                }
                freeIndexes.Add(i);
            }
            var index = Random.Range(0, freeIndexes.Count);
            _reservedIndexes.Enqueue(freeIndexes[index]);
            return buttonsData[freeIndexes[index]];
        }
    }
}
