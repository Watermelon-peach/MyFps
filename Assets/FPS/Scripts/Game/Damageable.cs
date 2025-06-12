using UnityEngine;

namespace Unity.FPS.Game
{
    //대미지를 입는 충돌체마다 부착시켜 대미지를 계산하는 클래스
    public class Damageable : MonoBehaviour
    {
        #region Variables
        //참조
        private Health health;

        //대미지 계수
        [SerializeField]
        private float damageMultiplier = 1.0f;

        //셀프 대미지 계수
        private float sensibilityToSelfDamage = 0.5f;
        #endregion

        #region Unity Event Method
        private void Awake()
        {
            //참조
            health = GetComponent<Health>();
            if (health == null)
            {
                health = GetComponentInParent<Health>();
            }
        }
        #endregion

        #region Custom Method
        public void InflictDamage(float damage, bool isExplosionDamage, GameObject damageSource)
        {
            if (health == null)
                return;

            var totalDamage = damage;

            //범위 공격이 아닌 경우에만 대미지 계수 적용
            if (!isExplosionDamage)
            {
                //대미지 계수 연산
                totalDamage *= damageMultiplier;
            }


            //셀프 대미지 체크
            if (health.gameObject == damageSource)
            {
                totalDamage *= sensibilityToSelfDamage;
            }

            //대미지 계산 후
            health.TakeDamage(totalDamage, damageSource);
        }
        #endregion
    }

}
