using UnityEngine;

namespace MyFps
{
    //맵에 떨어진 아이템 부딪혀서 먹기
    public class PickupItem : MonoBehaviour
    {
        #region Variables
        [SerializeField] private float rotateSpeed = 20f;
        //[SerializeField][Range(0f, 360f)] private float testVariable = 0f;
        [SerializeField] private float verticalBobFrequency = 1f;   //위 아래 이동속도
        [SerializeField] private float bobbingAmount = 1f;           //위 아래 진폭 크기

        private Vector3 startPosition;                                //아이템 초기 위치값
        #endregion

        #region Unity Event Method
        private void Start()
        {
            //초기화
            startPosition = transform.position;
        }
        private void Update()
        {
            /*testVariable += 100 * Time.deltaTime;
            if (testVariable >=360)
            {
                testVariable = 0f;
            }
            transform.Translate(Vector3.up * Mathf.Sin(testVariable*Mathf.Deg2Rad)*Time.deltaTime);*/

            //위 아래 이동
            float bobbingAnimationPhase = Mathf.Sin(Time.time * verticalBobFrequency) * bobbingAmount;
            transform.position = startPosition + Vector3.up * bobbingAnimationPhase;

            //아이템 회전
            transform.Rotate(Vector3.up, rotateSpeed* Time.deltaTime, Space.World);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                if (OnPickup())
                {
                    //킬
                    Destroy(gameObject);
                }
            }
        }
        #endregion

        #region Custom Method
        //조건을 체크하여 아이템을 먹으면 true, 못 먹으면 false
        protected virtual bool OnPickup()
        {
            //아이템을 먹을 수 있는지 체크
            //아이템 먹었을 때의 효과 구현
            return true;
        }

        #endregion
    }
}

/*
1. 플레이어가 부딪히는 충돌 체크 : 충돌하면
- 탄환 7개 지급
- 아이템 킬

2. 아이템 움직임 구현
- 아이템이 360도 회전
- 위 아래로 왔다 갔다 흔들림 구현 (Mathf : Sine 곡선 활용)
*/