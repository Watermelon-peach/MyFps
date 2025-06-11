using UnityEngine;
using UnityEngine.InputSystem;

namespace MyFps
{
    //게임 중 메뉴 관리 클래스
    public class PauseUI : MonoBehaviour
    {
        #region Variables
        public SceneFader fader;
        [SerializeField]
        private string sceneToLoad = "MainMenu";

        public GameObject pauseUI;
        public PlayerInput playerInput;
        #endregion

        #region Custom Method
        //esc키 누르면 UI 활성화, 다시 esc 키 누르면 UI 비활성화 - 토글키
        public void OnPause(InputAction.CallbackContext context)
        {
            pauseUI.SetActive(!pauseUI.activeSelf);
            if (pauseUI.activeSelf) //창이 열린 상태
            {
                Time.timeScale = 0f;
                
                //커서 제어
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else    //창이 닫힌 상태
            {
                Time.timeScale = 1f;
                
                //커서 제어
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

        //메뉴가기 버튼 호출
        public void MenuButton()
        {
            fader.FadeTo(sceneToLoad);
        }

        //게임 재개
        public void ContinueButton()
        {
            pauseUI.SetActive(false);
        }
        #endregion
    }

}
