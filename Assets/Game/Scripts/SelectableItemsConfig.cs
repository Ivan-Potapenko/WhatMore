using UnityEngine;

namespace Data {

    [CreateAssetMenu(fileName = nameof(SelectableItemsConfig), menuName = "Data/SelectableItemsConfig")]
    public class SelectableItemsConfig : AbstractSelectableItemsConfig {

        public enum Type {
            Music,
            Games,
            Trips,
            Education,
            Entertainment,
        }

        [SerializeField]
        private ChoseButtonData[] _choseButtonsData;
        public override ChoseButtonData[] ChoseButtonsData => _choseButtonsData;
    }
}

