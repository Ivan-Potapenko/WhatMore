using UnityEngine;
using UnityEngine.UI;

namespace Game {

    public class ResultIndicatorGroup : MonoBehaviour {

        [SerializeField]
        private Image[] _images;

        [SerializeField]
        private Color _winColor;

        [SerializeField]
        private Color _loseColor;

        public void ShowResult(bool win) {
            gameObject.SetActive(true);
            foreach (var image in _images) {
                image.color = win ? _winColor : _loseColor;
            }
        }

        public void HideResult() {
            gameObject.SetActive(false);
        }
    }
}
