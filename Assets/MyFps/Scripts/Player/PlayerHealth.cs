using UnityEngine;
using System.Collections;

namespace MyFps
{

    public class PlayerHealth : MonoBehaviour, IDamageable
    {
        #region Variables
        //체력
        private float currentHealth;
        [SerializeField]
        private float maxHealth = 20;

        private bool isDeath = false;

        //대미지 효과
        public GameObject damageFlash;
        public AudioSource[] hurtSfxs;

        //죽음 처리
        public SceneFader fader;
        [SerializeField]
        private string sceneToLoad = "GameOver";
        #endregion

        #region Unity Event Method
        private void Start()
        {
            //초기화
            currentHealth = maxHealth;
        }
        #endregion

        #region Custom Method
        public void TakeDamage(float damage)
        {
            currentHealth -= damage;

            //Debug.Log($"Player Current Health: {currentHealth}");
            //대미지 연출 (Sfx, Vfx)
            StartCoroutine(DamageEffect());

            if (currentHealth <= 0f && isDeath == false)
            {
                Die();
            }
        }

        //화면 전체 빨간색 플래시 효과
        //대미지 사운드 3개중 1 랜덤 발생
        IEnumerator DamageEffect()
        {
            //vfx
            damageFlash.SetActive(true);
            //카메라 흔들기
            CinemachineCameraShake.Instance.Shake(2f, 1f, 0.75f);

            int index = Random.Range(0, 3);
            hurtSfxs[index].Play();

            yield return new WaitForSeconds(1f);
            damageFlash.SetActive(false);
        }

        private void Die()
        {
            isDeath = true;

            //죽음처리
            fader.FadeTo(sceneToLoad);
        }
        #endregion
    }

}
