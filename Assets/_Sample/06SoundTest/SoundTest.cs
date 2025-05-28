
using UnityEngine;

namespace MySample
{
    public class SoundTest : MonoBehaviour
    {
        #region Variables
        private AudioSource audioSource;

        //Audio Source 속성
        public AudioClip clip;
        [SerializeField]
        private float volume = 1.0f;
        [SerializeField]
        private float pitch = 1.0f;
        [SerializeField]
        private bool loop = false;

        #endregion

        #region Unity Event Method
        private void Start()
        {
            //참조
            audioSource = GetComponent<AudioSource>();

            //
            //SoundOneShot();
            SoundPlay();
        }
        #endregion

        #region Custom Method
        //사운드 플레이
        private void SoundOneShot()
        {
            //audioSource.Play();
            audioSource.PlayOneShot(clip);
        }

        private void SoundPlay()
        {
            //Audio Source 속성 셋팅
            audioSource.clip = clip;
            audioSource.volume = volume;
            audioSource.pitch = pitch;
            audioSource.loop = loop;

            //사운드 플레이
            audioSource.Play();
        }
        #endregion
    }

}
