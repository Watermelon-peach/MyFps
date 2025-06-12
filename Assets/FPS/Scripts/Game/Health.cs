using UnityEngine;
using UnityEngine.Events;

namespace Unity.FPS.Game
{
    //체력을 관리하는 클래스
    public class Health : MonoBehaviour
    {
        #region Variables
        [SerializeField]
        private float maxHealth = 100f;

        private bool isDeath = false;

        [SerializeField]
        private float criticalHealthRatio = 0.2f;

        public UnityAction<float> OnHeal;                   //힐하면 등록된 함수를 호출한다
        public UnityAction<float, GameObject> OnDamaged;    //대미지를 입으면 등록된 함수를 호출한다
        public UnityAction OnDie;                           //죽을 때 등록된 함수를 호출한다
        #endregion

        #region Property
        public float CurrentHealth { get; private set; }

        //무적 체크
        public bool Invincible { get; set; }
        #endregion

        #region Unity Event Method
        private void Start()
        {
            //초기화
            CurrentHealth = maxHealth;
            Invincible = false;
        }
        #endregion

        #region Custom Method
        //힐 아이템을 먹을 수 있는지 체크
        public bool CanPickUp() => CurrentHealth < maxHealth;

        //UI HP바 게이지 량
        public float GetRatio() => CurrentHealth / maxHealth;

        //위험 체크
        public bool IsCritical() => GetRatio() <= criticalHealthRatio;

        //힐 계산
        public void Heal(float healAmount)
        {
            //실제로 받는 힐 계산
            float beforeHealth = CurrentHealth; //힐 받기 전의 체력
            CurrentHealth += healAmount;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0f, maxHealth);  //체력의 최대 최소값 클램프

            //리얼 힐
            float realHeal = beforeHealth - CurrentHealth;
            if (realHeal > 0)
            {
                //힐 구현
                OnHeal?.Invoke(realHeal);
            }
        }
        //매개변수 대미지량, 대미지를 준 오브젝트
        public void TakeDamage(float damage, GameObject damageSource)
        {
            //무적 체크
            if (Invincible == true)
                return;
            //실제로 입은 대미지 계산
            float beforeHealth = CurrentHealth; //대미지 입기 전의 체력
            CurrentHealth -= damage;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0f, maxHealth);  //체력의 최대 최소값 클램프

            //리얼 대미지
            float realDamage = beforeHealth - CurrentHealth;
            if (realDamage > 0f)
            {
                //대미지 효과 구현
                OnDamaged?.Invoke(realDamage, damageSource);
            }
            //죽음 처리
            HandleDeath();
        }

        private void HandleDeath()
        {
            if (isDeath)
                return;
            if(CurrentHealth <= 0)
            {
                isDeath = true;

                //죽음 - 등록된 함수를 호출한다
                OnDie?.Invoke();
            }
        }
        #endregion
    }

}
