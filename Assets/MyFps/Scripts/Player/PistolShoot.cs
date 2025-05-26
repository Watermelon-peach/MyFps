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

        //총구발사 이펙트
        public ParticleSystem muzzleFlash;
        //피격 이펙트 - 탄착지점에서 이펙트 효과 발생
        public GameObject hitImpactPrefab;
        //hit 충격 강도
        [SerializeField]
        private float impactForce = 10f;

        public AudioSource fireSound;
        public Transform firePoint;

        //딜레이 타이머 (연사 방지)
        private bool isFired = false;
        [SerializeField]
        private float fireDelay = 1f;
        private float timeCount = 0f;

        private float maxDistance = 200f;
        [SerializeField]
        private float attackDamage = 5f;
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
        public void Fire()
        {
            if (isFired)
                return;

            if (PlayerDataManager.Instance.UseAmmo(1))
                StartCoroutine(Shoot());
        }

        //슛
        IEnumerator Shoot()
        {
            isFired = true;

            //레이를 쏴서 200 안에 적(로봇)이 있으면 적에게 대미지를 준다
            RaycastHit hit;
            bool isHit = Physics.Raycast(firePoint.position, firePoint.TransformDirection(Vector3.forward), out hit);
            if (isHit)
            {
                //Debug.Log($"{hit.transform.name}에게 {attackDamage} 대미지를 준다");
                /*Robot robot = hit.transform.GetComponent<Robot>();
                if(robot)
                {
                    robot.TakeDamage(attackDamage);
                }*/
                //hit.point
                if (hitImpactPrefab)
                {
                    GameObject effectGo = Instantiate(hitImpactPrefab, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(effectGo, 2f);
                }

                if (hit.rigidbody)
                {
                    hit.rigidbody.AddForce(-hit.normal * impactForce, ForceMode.Impulse);
                }
                    
                IDamageable damageable = hit.transform.GetComponent<IDamageable>();
                if (damageable != null)
                {
                
                    damageable.TakeDamage(attackDamage);
                }
            }

            //애니메이션 플레이
            animator.SetTrigger("Fire");
            
            //연출 Vfx,Sfx
            //발사 이펙트 플래시 활성화
            muzzleEffect.SetActive(true);
            if (muzzleFlash)
            {
                muzzleFlash.Play();
            }
            //발사 사운드 플레이
            fireSound.Play();

            //0.5초 딜레이
            yield return new WaitForSeconds(0.1f);

            if (muzzleFlash)
            {
                muzzleFlash.Stop();
            }
            //발사 이펙트 플래시 비활성화
            muzzleEffect.SetActive(false);
        }

        
        #endregion
    }

}
