using UnityEngine;

namespace Data {

    [CreateAssetMenu(fileName = nameof(SelectableItemsConfig), menuName = "Data/SelectableItemsConfig")]
    public class SelectableItemsConfig : ScriptableObject {

        [SerializeField]
        private ChoseButtonData[] _choseButtonsData;
        public ChoseButtonData[] ChoseButtonsData => _choseButtonsData;
    }
}

