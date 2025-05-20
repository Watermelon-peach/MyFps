using System.Collections;
using TMPro;
using UnityEngine;

namespace MyFps
{
    //플레이 씬 오프닝 연출
    public class AOpenning : MonoBehaviour
    {
        #region Variables
        //플레이어 오브젝트
        public GameObject playerObject;
        //페이더 객체
        public SceneFader fader;

        //시나리오 대사 처리
        public TextMeshProUGUI SequenceText;

        [SerializeField] private string sequence01 = "I need to get out of here";

        [SerializeField] private float SequenceDelay = 0f;
        #endregion

        #region Unity Event Method
        private void Start()
        {
            //커서 제어
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            //오프닝 연출 시작
            StartCoroutine(SequencePlay());
        }
        #endregion

        #region Custom Method
        //오프닝 연출
        IEnumerator SequencePlay()
        {
            //0.플레이 캐릭터 비활성화
            playerObject.SetActive(false);

            //1. 페이드인 연출 (1초 대기 후 페이드인 효과)
            fader.FadeStart(1f);

            //2.화면 하단에 시나리오 텍스트 화면 출력(3초)
            SequenceText.text = sequence01;

            //3. 3초 후에 시나리오 텍스트 없어진다
            yield return new WaitForSeconds(SequenceDelay);
            SequenceText.text = "";
            
            //4. 플레이 캐릭터 활성화
            playerObject.SetActive(true);
        }
        #endregion
    }
}
