using UnityEngine;

namespace MyFps
{
    //게임오버 처리: 메뉴가기, 다시하기
    public class GameOver : MonoBehaviour
    {
        #region Variables
        public SceneFader fader;
        [SerializeField]
        private string sceneToLoad = "PlayScene";
        #endregion

        #region Unity Event Method
        private void Start()
        {
            //커서 제어
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            //FadeIn 효과
            fader.FadeStart();
        }
        #endregion

        #region Custom Method
        public void Retry()
        {
            int nowScene = PlayerDataManager.Instance.SceneNumber;
            fader.FadeTo(nowScene);
        }

        public void Menu()
        {
            fader.FadeTo(sceneToLoad);
        }
        #endregion
    }

}
