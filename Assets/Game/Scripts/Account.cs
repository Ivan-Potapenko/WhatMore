using Data;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using YG;
using PlayerPrefs = RedefineYG.PlayerPrefs;

namespace Game {

    public class Account : SingletonCrossScene<Account> {

        private int _bestScore;
        public int BestScore {
            get { return _bestScore; }
            set {
                SaveAccount();
                YG2.SetLeaderboard("BestScore", value);
                _bestScore = value;
            }
        }

        private int _money;
        public int Money {
            get { return _money; }
            set {
                _money = value;
                _moneyCountLabel.text = _money.ToString();
                SaveAccount();
            }
        }

        private Dictionary<SelectableItemsConfig.Type, bool> _openedTypes = new Dictionary<SelectableItemsConfig.Type, bool>();
        public Dictionary<SelectableItemsConfig.Type, bool> OpenedTypes {
            get { return _openedTypes; }
            set {
                _openedTypes = value;
                SaveAccount();
            }
        }

        [SerializeField]
        private TextMeshProUGUI _moneyCountLabel;

        public SelectableItemsConfig currentSelectableItemsConfig;

        protected override void Init() {
            base.Init();
            LoadAccountData();
            _moneyCountLabel.text = _money.ToString();
        }

        private void LoadAccountData() {
            _bestScore = PlayerPrefs.GetInt(nameof(_bestScore), 0);
            _money = PlayerPrefs.GetInt(nameof(_money), 0);
            foreach (var type in Enum.GetValues(typeof(SelectableItemsConfig.Type)).Cast<SelectableItemsConfig.Type>()) {
                _openedTypes.Add(type, Convert.ToBoolean(PlayerPrefs.GetInt(nameof(_openedTypes) + type, 0)));
            }
        }

        private void SaveAccount() {
            PlayerPrefs.SetInt(nameof(_bestScore), _bestScore);
            PlayerPrefs.SetInt(nameof(_money), _money);
            foreach (var type in _openedTypes) {
                PlayerPrefs.SetInt(nameof(_openedTypes) + type.Key, Convert.ToInt32(type.Value));
            }
            PlayerPrefs.Save();
        }
    }
}

