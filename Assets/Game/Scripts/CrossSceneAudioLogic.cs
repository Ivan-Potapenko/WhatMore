using UnityEngine;
using Infrastructure;

namespace Game {

    public class CrossSceneAudioLogic : SingletonCrossScene<CrossSceneAudioLogic> {

        [SerializeField]
        private AudioSource _buttonClickedAudio;

        public void PlayButtonClickedSound() {
            _buttonClickedAudio.Play();
        }
    }
}