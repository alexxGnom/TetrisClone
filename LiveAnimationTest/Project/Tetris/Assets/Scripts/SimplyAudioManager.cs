using UnityEngine;

namespace Tetris
{
    public interface IAudioManager
    {
        void PlayOnce(string name);
    }

    public class SimplyAudioManager : MonoBehaviour, IAudioManager
    {
        #region Unity properties

        [SerializeField]
        private AudioClip[] _clips;

        #endregion

        #region Private fields

        private AudioSource _audioSource;

        #endregion

        #region Interface

        public void PlayOnce(string name)
        {
            var clip = GetClipByName(name);
            if (clip != null)
                _audioSource.PlayOneShot(clip);
        }

        #endregion

        #region Utils

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private AudioClip GetClipByName(string name)
        {
            foreach (var clip in _clips)
            {
                if (clip.name == name)
                    return clip;
            }
            return null;
        }

        #endregion

    }
}
