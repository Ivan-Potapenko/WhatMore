using Data;
using Infrastructure;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game {

    public class ComparisonItem : MonoBehaviour, IPointerClickHandler {

        [SerializeField]
        private TextMeshProUGUI _nameLabel;
        [SerializeField]
        private TextMeshProUGUI _valueLabel;
        [SerializeField]
        private AsyncImage _image;

        [SerializeField]
        private RectTransform _rectTransform;
        public RectTransform RectTransform => _rectTransform;

        public event Action onClick;

        public ChoseButtonData Data { get; private set; }

        public void SetData(ChoseButtonData data) {
            Data = data;
            _nameLabel.text = data.Name;
            _valueLabel.text = data.Value.ToString();
            _valueLabel.gameObject.SetActive(false);
            _image.Load(data.ImageAssetReference);
        }

        public void OnPointerClick(PointerEventData eventData) {
            onClick?.Invoke();
        }

        public void ProcessValueShow(float percent) {
            _valueLabel.gameObject.SetActive(true);
            _valueLabel.text = ((long)(Data.Value * percent)).ToString("##,##,##,###");
        }

        public void ShowResult(bool rightAnswer) {
            ProcessValueShow(1);
            if (rightAnswer) {
                return;
            }
        }
    }
}
