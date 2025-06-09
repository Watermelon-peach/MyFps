using UnityEngine;
using Unity.Cinemachine;
using System.Collections;
using System.Runtime.InteropServices;

namespace MyFps
{
    //인트로 연출 구현
    public class Intro : MonoBehaviour
    {
        #region Variables
        //참조
        public SceneFader fader;
        [SerializeField] private string sceneToLoad = "MainScene01";
        private SplineAutoDolly.FixedSpeed dolly;

        //이동
        public CinemachineSplineCart cart;  //돌리 카트
        private bool[] isArrived;           //이동 포인트 지점에 도착했는지 여부 체크
        [SerializeField] private int wayPointIndex;          //다음 이동 목표 지점

        //연출
        public Animator animator;
        public GameObject introUI;
        public GameObject theShedLight;

        [SerializeField] private string aroundTrigger = "Around";
        #endregion

        #region Unity Event Method
        private void Start()
        {
            //초기화
            isArrived = new bool[5];
            wayPointIndex = 0;
            dolly = cart.AutomaticDolly.Method as SplineAutoDolly.FixedSpeed;
            dolly.Speed = 0f;
            
            //시퀀스
            StartCoroutine(PlayerStartSequence());
        }

        private void Update()
        {
            //도착 판정
            if (cart.SplinePosition >= wayPointIndex && !isArrived[wayPointIndex])
            {
                Arrive();
            }

            //스킵버튼
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //플레이씬 가기
                GoToPlayScene();
            }
        }
        #endregion

        #region Custom Method
        //목표 지점 도착
        private void Arrive()
        {
            //마지막 엔드 포인트 지점 도착 체크
            if (wayPointIndex == isArrived.Length -1 )
            {
                StartCoroutine(PlayEndSequence());
            }
            else
            {
                StartCoroutine(PlayStaySequence());
            }
        }

        //플레이씬 가기
        private void GoToPlayScene()
        {
            //코루틴 종료
            StopAllCoroutines();

            //배경음 종료
            AudioManager.Instance.StopBgm();

            //다음 씬 가기
            fader.FadeTo(sceneToLoad);
        }
        //시작 시퀀스
        IEnumerator PlayerStartSequence()
        {
            isArrived[0] = true;

            //페이드인 효과
            fader.FadeStart();
            //배경음 시작
            AudioManager.Instance.PlayBgm("IntroBgm");
            yield return new WaitForSeconds(1f);

            //둘러보기
            animator.SetTrigger(aroundTrigger);
            yield return new WaitForSeconds(4f);

            //이동시작
            wayPointIndex = 1;  //다음 목표지점 설정
            dolly.Speed = 0.1f;
        }

        //이동 포인트 지점 도착 시퀀스
        IEnumerator PlayStaySequence()
        {
            //도착 체크
            isArrived[wayPointIndex] = true;

            //이동 멈춤
            dolly.Speed = 0f;
            
            //둘러보기
            animator.SetTrigger(aroundTrigger);
            yield return new WaitForSeconds(4f);

            switch(wayPointIndex)
            {
                case 1:
                    introUI.SetActive(true);
                    break;
                case 2:
                    introUI.SetActive(false);
                    break;
                case 3:
                    theShedLight.SetActive(true);
                    break;
            }

            //이동 시작
            wayPointIndex++;
            dolly.Speed = 0.1f;
        }

        //최종지점 도착 시퀀스
        IEnumerator PlayEndSequence()
        {
            //오두막 라이트 끄고 다음 씬으로 이동, 배경음 종료

            //도착 체크
            isArrived[wayPointIndex] = true;

            //이동 멈춤
            dolly.Speed = 0f;
            yield return new WaitForSeconds(5f);    

            //오두막 라이트 끄고
            theShedLight.SetActive(false);

            yield return new WaitForSeconds(2f);

            //배경음 종료
            AudioManager.Instance.StopBgm();

            //다음 씬 가기
            fader.FadeTo(sceneToLoad);
        }
        #endregion
    }

}
