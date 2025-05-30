using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

namespace MyFps
{
    //공격을 받으면 (대미지를 입으면) 오브젝트가 부서진다
    //오브젝트 부서지는 연출, 두번다시 공격을 받지 않아야한다
    //부서질 때 그릇 깨지는 사운드 플레이
    //아이템(key) 숨기기
    public class BreakableObject : MonoBehaviour, IDamageable
    {
        #region Variables
        public GameObject fakeObject;
        public GameObject realObject;
        public GameObject sphereObject;

        private bool isDeath = false;   //두번 죽는 것 체크
        private float health = 1f;

        private string breakSfx = "BreakSound";

        //숨겨진 아이템
        public GameObject hiddenItemPrefab;
        [SerializeField]
        private Vector3 offset;

        public GameObject hiddenItem;

        [SerializeField]
        private bool unBreakable;
        #endregion

        #region Unity Event Method

        #endregion

        #region Custom Method
        public void TakeDamage(float damage)
        {
            //무적모드
            if (unBreakable)
                return;

            health -= damage;

            if (health <= 0f && !isDeath)
            {
                Die();
            }
            
        }
        private void Die()
        {
            isDeath = true;

            //깨지는 연출
            StartCoroutine(Break());
        }

        IEnumerator Break()
        {
            //충돌체 제거
            GetComponent<BoxCollider>().enabled = false;

            //깨지는 오브젝트 보이기
            fakeObject.SetActive(false);

            if (sphereObject)
            {
                yield return new WaitForSeconds(0.1f);
                sphereObject.SetActive(true);

            }
            realObject.SetActive(true);

            //사운드
            AudioManager.Instance.Play(breakSfx);

            if (sphereObject)
            {
                yield return new WaitForSeconds(0.1f);
                sphereObject.SetActive(false);
            }

            //숨겨진 아이템 나타내기
            /*if (hiddenItemPrefab)
            {
                Instantiate(hiddenItemPrefab, transform.position + offset, Quaternion.identity);
            }*/

            if (hiddenItem)
            {
                hiddenItem.SetActive(true);
            }
        }
        #endregion
    }

}
