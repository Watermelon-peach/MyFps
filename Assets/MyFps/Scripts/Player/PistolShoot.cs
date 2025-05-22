using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MyFps
{
    //피스톨 제어 클래스
    public class PistolShoot : MonoBehaviour
    {
        #region Variables
        //참조
        private Animator animator;
        public GameObject muzzleEffect;
        public AudioSource fireSound;
        public Transform firePoint;

        //딜레이 타이머 (연사 방지)
        private bool isFired = false;
        [SerializeField]
        private float fireDelay = 1f;
        private float timeCount = 0f;

        #endregion

        #region Unity Event Method
        private void Awake()
        {
            //참조
            animator = GetComponent<Animator>();
        }
        private void Start()
        {
            //초기화
            isFired = false;
        }
        private void Update()
        {
            if (isFired)
            {
                timeCount += Time.deltaTime;
                if (timeCount >= fireDelay)
                {
                    isFired = false;
                    timeCount = 0f;
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            RaycastHit hit;
            float maxDistance = 200f;
            bool isHit = Physics.Raycast(firePoint.position, firePoint.TransformDirection(Vector3.forward), out hit);

            Gizmos.color = Color.red;
            if (isHit)
            {
                Gizmos.DrawRay(firePoint.position, firePoint.forward * hit.distance);
            }
            else
            {
                Gizmos.DrawRay(firePoint.position, firePoint.forward * maxDistance);
            }
        }
        #endregion

        #region Custom Method

        public void OnFire(InputAction.CallbackContext context)
        {
            if(context.started && !isFired) //keydown, buttondown
            {
                StartCoroutine(Fire());
            }
        }

        IEnumerator Fire()
        {
            isFired = true;
            //애니메이션 플레이
            animator.SetTrigger("Fire");
            //발사 이펙트 플래시 활성화
            muzzleEffect.SetActive(true);
            //발사 사운드 플레이
            fireSound.Play();
            //0.5초 딜레이
            yield return new WaitForSeconds(0.5f);
            //발사 이펙트 플래시 비활성화
            muzzleEffect.SetActive(false);
        }

        
        #endregion
    }

}
