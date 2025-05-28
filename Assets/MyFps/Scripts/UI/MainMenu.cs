using UnityEngine;

namespace MyFps
{
    //메인 메뉴 씬을 관리하는 클래스
    public class MainMenu : MonoBehaviour
    {
        #region Variables
        //참조
        private AudioManager audioManager;
        public SceneFader fader;
        [SerializeField]private string sceneToLoad = "MainScene01";
        #endregion

        #region Unity Event Method
        private void Start()
        {
            //참조
            audioManager = AudioManager.Instance;

            //씬 시작 시 페이드 인 효과
            fader.FadeStart();

            //메뉴 배경음 플레이
            audioManager.Play("MenuMusic");
        }
        #endregion

        #region Custom Method
        public void NewGame()
        {
            //메뉴 선택 사운드
            audioManager.Play("MenuSelect");
            //새 게임 하러가기
            fader.FadeTo(sceneToLoad);
        }
        
        public void LoadGame()
        {
            //메뉴 선택 사운드
            audioManager.Play("MenuSelect");
            Debug.Log("LOAD GAME!!!");
        }

        public void Options()
        {
            //메뉴 선택 사운드
            audioManager.Play("MenuSelect");
            Debug.Log("Show Options");
        }

        public void Credits()
        {
            //메뉴 선택 사운드
            audioManager.Play("MenuSelect");
            Debug.Log("Show Credits");
        }

        public void QuitGame()
        {
            //메뉴 선택 사운드
            audioManager.Play("MenuSelect");
            Debug.Log("QUIT GAME");
            Application.Quit();
        }
        #endregion
    }
}
