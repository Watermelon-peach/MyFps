using UnityEngine;

namespace MyFps
{
    //트리거가 작동하면 메인 메뉴 보내기
    public class FExitTrigger : MonoBehaviour
    {
        #region Variables
        public SceneFader fader;
        #endregion

        #region Unity Event Method
        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.tag == "Player")
            {
                //BGM 정지
                AudioManager.Instance.StopBgm();
                //커서 제어
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                //메인메뉴 가기
                fader.FadeTo("MainMenu");
            }
        }
        #endregion
    }
}
