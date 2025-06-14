using UnityEngine;

namespace Unity.FPS.Game
{
    //무기를 제어하는 클래스, 모든 무기에 부착된다
    [RequireComponent(typeof(AudioSource))]
    public class WeaponController : MonoBehaviour
    {
        #region Variables
        //무기 비주얼 활성화, 비활성화
        public GameObject weaponRoot;

        private AudioSource shootAudioSource;
        public AudioClip switchWeaponSfx;       //무기 교환 시 효과음
        #endregion

        #region Property
        public GameObject Owner { get; set; }   //무기를 장착한 주인 오브젝트
        public GameObject SourcePrefab { get; set; }    //무기를 생성한 원본 프리팹
        public bool IsWeaponActive { get; set; }        //현재 이 무기가 액티브 무기
        #endregion 

        #region Unity Event Method
        private void Awake()
        {
            //참조
            shootAudioSource = GetComponent<AudioSource>();
        }
        #endregion

        #region Custom Mehtod
        public void ShowWeapon(bool show)
        {
            weaponRoot.SetActive(show);
            IsWeaponActive = show;
            //무기교체
            if (show)
            {
                shootAudioSource.PlayOneShot(switchWeaponSfx);
            }
        }
        #endregion
    }

}
