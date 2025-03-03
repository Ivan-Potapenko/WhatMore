using System.Linq;
using UnityEngine;

namespace Data {

    [CreateAssetMenu(fileName = nameof(MultiSelectableItemsConfig), menuName = "Data/MultiSelectableItemsConfig")]
    public class MultiSelectableItemsConfig : AbstractSelectableItemsConfig {

        [SerializeField]
        private AbstractSelectableItemsConfig[] _abstractSelectableItemsConfigs;

        public override ChoseButtonData[] ChoseButtonsData {
            get {
                if (_choseButtonsDataCash == null || _choseButtonsDataCash.Length == 0) {
                    _choseButtonsDataCash = _abstractSelectableItemsConfigs.SelectMany(config => config.ChoseButtonsData).ToArray();
                }
                return _choseButtonsDataCash;
            }
        }

        private ChoseButtonData[] _choseButtonsDataCash = null;
    }
}

