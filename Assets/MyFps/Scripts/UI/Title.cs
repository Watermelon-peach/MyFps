using System.Collections;
using UnityEngine;

namespace MyFps
{
    //타이틀 씬을 관리하는 클래스 : 3초 후에 애니키 보이고 10초 후에 메인메뉴 가기
    public class Title : MonoBehaviour
    {
        #region Variables
        public SceneFader fader;
        public GameObject anyKeyText;

        [SerializeField] private string sceneToLoad = "MainMenu";

        #endregion

        #region Unity Event Method
        private void Start()
        {
            //페이드 인
            fader.FadeStart();

            //배경음 플레이
            AudioManager.Instance.PlayBgm("TitleBgm");

            //코루틴 함수 실행
            StartCoroutine(ShowAnyKey());
        }

        private void Update()
        {
            //애니키가 보인 후에 아무 키나 누르면 메인메뉴 가기 - old Input
            if (Input.anyKeyDown && anyKeyText.activeSelf)
            {
                AudioManager.Instance.Stop("TitleBgm");
                fader.FadeTo(sceneToLoad);
            }
        }
        #endregion

        #region Custom Method
        //코루틴 함수 : 3초 후에 애니키 보이고 10초 후에 메인메뉴 가기
        IEnumerator ShowAnyKey()
        {
            yield return new WaitForSeconds(3f);
            anyKeyText.SetActive(true);

            yield return new WaitForSeconds(10f);

            AudioManager.Instance.Stop("TitleBgm");
            fader.FadeTo(sceneToLoad);
        }
        #endregion
    }

}
