using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
namespace MyFps
{
    public class RobotHealth : MonoBehaviour, IDamageable
    {
        #region Variables
        //체력
        private float currentHealth;
        [SerializeField]
        private float maxHealth = 20;

        //킬 딜레이
        [SerializeField]
        private float destroyDelay = 6f;
        private bool isDeath = false;

        //죽음시 호출되는 이벤트 함수
        public UnityAction OnDie;
        #endregion

        #region Property
        public bool IsDeath => isDeath;
        #endregion

        #region Unity Event Method
        private void Start()
        {
            //초기화
            currentHealth = maxHealth;
        }
        #endregion

        #region Custom Method

        #endregion

        public void TakeDamage(float damage)
        {
            currentHealth -= damage;

            //대미지 연출 (Sfx, Vfx)

            if (currentHealth <= 0f && isDeath == false)
            {
                Die();
            }
        }

        void Die()
        {
            isDeath = true;

            //죽음 시 등록되는 함수 호출
            OnDie?.Invoke();

            //킬
            Destroy(gameObject, destroyDelay);

            //보상 처리..
        }
    }

}
