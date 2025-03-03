using UnityEngine;

namespace Data {

    public abstract class AbstractSelectableItemsConfig : ScriptableObject {

        public abstract ChoseButtonData[] ChoseButtonsData { get; }
    }
}
