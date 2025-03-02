using UnityEngine;

namespace Data {

    [CreateAssetMenu(fileName = nameof(SelectableItemsConfig), menuName = "Data/SelectableItemsConfig")]
    public class SelectableItemsConfig : ScriptableObject {

        public enum Type {
            Music,
            Games,
            Trips,
            Education,
            Entertainment,
        }

        [SerializeField]
        private ChoseButtonData[] _choseButtonsData;
        public ChoseButtonData[] ChoseButtonsData => _choseButtonsData;
    }
}

