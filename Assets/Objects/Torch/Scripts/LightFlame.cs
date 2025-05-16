using UnityEngine;
using System.Collections;

namespace MyFps
{
    public class LightFlame : MonoBehaviour
    {
        #region Variables
        //애니메이터
        public Animator animator;

        //애니메이션 모드
        private int lightMode = 0;
        #endregion

        private void Start()
        {
            //초기화
            lightMode = 0;
        }

        private void Update()
        {
            if (lightMode == 0)
            {
                StartCoroutine("RandomAnimation");
            }
        }

        IEnumerator RandomAnimation()
        {
            lightMode = Random.Range(1, 4);
            animator.SetInteger("LightMode", lightMode);

            yield return new WaitForSeconds(0.99f);
            lightMode = 0;
        }
    }

}
