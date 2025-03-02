using Game;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace UI {
    public class MoneyRewardButton : MonoBehaviour {

        [SerializeField]
        private Button _button;

        [SerializeField]
        private int _money;

        private void Start() {
            _button.onClick.AddListener(OnButtonClicked);
        }

        private void OnButtonClicked() {
            YG2.RewardedAdvShow("Money", () => {
                Account.Instance.Money += _money;
            });
        }
    }
}

