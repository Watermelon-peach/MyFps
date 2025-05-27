using UnityEngine;

namespace MyFps
{
    //다음 씬 넘어가기, 문을 열면 문 여는 소리, 배경음 종료, 다음 씬으로 이동
    public class DoorCellExit : Interactive
    {
        #region Variables

        //애니메이션
        public Animator animator;

        //애니메이션 파라미터 스트링
        private string paramIsOpen = "IsOpen";

        //문 여는 소리
        public AudioSource audioSource;
        //배경음
        public AudioSource bgm01;

        public SceneFader fader;
        [SerializeField] private string sceneToLoad = "MainScene02";
        #endregion

        #region Custom Method
        protected override void DoAction()
        {
            GetComponent<BoxCollider>().enabled = false;

            //문 열고, 충돌체 제거
            animator.SetBool(paramIsOpen, true);        //문 여는 애니메이션 연출

            //배경음 종료
            bgm01.Stop();
            //문 여는 소리
            audioSource.Play();

            //씬 종료시 처리할 내용 구현
            //...

            fader.FadeTo(sceneToLoad);
        }
        #endregion
    }

}
