using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Data {

    [Serializable]
    public class ChoseButtonData {

        [SerializeField]
        private string _name;
        public string Name => _name;

        [SerializeField]
        private long _value;
        public long Value => _value;

        [SerializeField]
        private AssetReference _imageAssetReference;
        public AssetReference ImageAssetReference => _imageAssetReference;

    }
}
