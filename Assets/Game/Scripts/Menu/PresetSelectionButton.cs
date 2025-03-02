using Data;
using Game;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

namespace UI {

    public class PresetSelectionButton : MonoBehaviour {

        [SerializeField]
        private SelectableItemsConfig _itemsConfig;

        [SerializeField]
        private SelectableItemsConfig.Type _type;

        [SerializeField]
        private GameObject _byGroup;

        [SerializeField]
        private Button _button;

        [SerializeField]
        private int _price;

        [SerializeField]
        private bool _alwaysOpened;

        [SerializeField]
        private TextMeshProUGUI _moneyLabel;

        private void Awake() {
            _button.onClick.AddListener(OnButtonClicked);
        }

        private void OnEnable() {
            _byGroup.gameObject.SetActive(!Account.Instance.OpenedTypes[_type] && !_alwaysOpened);
            _moneyLabel.text = _price.ToString();
        }

        private void OnButtonClicked() {
            if(Account.Instance.OpenedTypes[_type] || _alwaysOpened) {
                StartBattle();
                return;
            }
            ByPreset();
        }

        private void ByPreset() {
            if(Account.Instance.Money < _price) {
                return;
            }
            Account.Instance.Money -= _price;
            Account.Instance.OpenedTypes[_type] = true;
            _byGroup.gameObject.SetActive(false);
        }

        private void StartBattle() {
            YG2.InterstitialAdvShow();
            Account.Instance.currentSelectableItemsConfig = _itemsConfig;
            SceneManager.LoadScene("GameScene");
        }
    }
}

