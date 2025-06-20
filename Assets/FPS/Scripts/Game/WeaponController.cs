using UnityEngine;
using System;

namespace Unity.FPS.Game
{
    //크로스헤어 데이터 구조체
    [Serializable]
    public struct CrosshairData
    {
        public Sprite crosshairSprite;
        public float crosshairSize;
        public Color crosshairColor;
    }

    public enum WeaponShootType
    {
        Manual,
        Automatic,
        Charge
    }

    //무기를 제어하는 클래스, 모든 무기에 부착된다
    [RequireComponent(typeof(AudioSource))]
    public class WeaponController : MonoBehaviour
    {
        #region Variables
        //무기 비주얼 활성화, 비활성화
        public GameObject weaponRoot;
        public Transform weaponMuzzle;

        private AudioSource shootAudioSource;
        public AudioClip switchWeaponSfx;       //무기 교환 시 효과음

        //크로스 헤어
        public CrosshairData defaultCrosshair;      //기본 크로스헤어
        public CrosshairData targetInSightCrosshair;    //적이 타겟팅 되었을 때의 크로스헤어

        //조준 Aim
        [Range(0, 1)]
        public float aimZoomRatio = 1f;     //조준시 줌 확대 배율
        public Vector3 aimOffset;           //조준 위치로 이동시 무기별 offset(조정) 위치값

        //슛팅
        [SerializeField] private WeaponShootType shootType;

        [SerializeField] private float maxAmmo = 8f;
        private float currentAmmo;

        [SerializeField] private float delayBetweenShots = 0.5f; //연사 시 발사 간격
        private float lastTimeShot;

        //발사 효과
        public GameObject muzzleFlashPrefab;    //vfx
        public AudioClip shootSfx;              //sfx 총기 발사 효과음

        //반동 Recoil
        public float recoilForce = 0.5f;

        //발사체 Projectile
        private Vector3 lastMuzzlePosition;     //지난 프레임 Muzzle의 위치
        #endregion

        #region Property
        public GameObject Owner { get; set; }   //무기를 장착한 주인 오브젝트
        public GameObject SourcePrefab { get; set; }    //무기를 생성한 원본 프리팹
        public bool IsWeaponActive { get; set; }        //현재 이 무기가 액티브한지
        
        //Projectile
        public Vector3 MuzzleWorldVelocity { get; private set; }

        public float CurrentCharge { get; private set; }
        #endregion 

        #region Unity Event Method
        private void Awake()
        {
            //참조
            shootAudioSource = GetComponent<AudioSource>();
        }
        private void Start()
        {
            //초기화
            currentAmmo = maxAmmo;
            lastTimeShot = Time.time;
        }

        private void Update()
        {
            if (Time.deltaTime >0f)
            {
                //이번 프레임의 Muzzle 속도 구하기
                MuzzleWorldVelocity = (weaponMuzzle.position - lastMuzzlePosition) / Time.deltaTime;

                //Muzzle 위치 저장
                lastMuzzlePosition = weaponMuzzle.position;
            }
            

            
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

        //슛 인풋 처리: 매개변수로 fire down, held, released 받아서 슈팅 타입 처리
        public bool HandleShootInput(bool inputDown, bool inputHeld, bool inputUp)
        {
            switch (shootType)
            {
                case WeaponShootType.Manual:
                    if(inputDown)
                    {
                        return TryShoot();
                    }
                    break;
                case WeaponShootType.Automatic:
                    if (inputHeld)
                    {
                        return TryShoot();
                    }
                    break;
                case WeaponShootType.Charge:
                    
                    break;
            }

            return false;
        }

        //발사 처리
        private bool TryShoot()
        {
            
            if (currentAmmo >= 1f && (lastTimeShot + delayBetweenShots) < Time.time)
            {
                currentAmmo -= 1f;
                Debug.Log($"currentAmmo: {currentAmmo}");

                HandleShoot();
                return true;
            }

            return false;
        }

        //발사 연출
        private void HandleShoot()
        {
            if (muzzleFlashPrefab)
            {
                //vfx - Muzzle effect
                GameObject effectGo = Instantiate(muzzleFlashPrefab, weaponMuzzle.position, weaponMuzzle.rotation, weaponMuzzle);
                Destroy(effectGo, 1f);
            }

            //sfx
            if (shootSfx)
            {
                shootAudioSource.PlayOneShot(shootSfx);
            }
            
            //발사한 시간 저장
            lastTimeShot = Time.time;
        }
        #endregion
    }

}
