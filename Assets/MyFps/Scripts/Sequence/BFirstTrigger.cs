using System.Collections;
using TMPro;
using UnityEngine;

namespace MySample
{
    //첫번재 트리거 연출
    public class BFirstTrigger : MonoBehaviour
    {
        #region Variables
        public GameObject Arrow;

        //플레이어 오브젝트
        public GameObject playerObject;

        //시나리오 대사 처리
        public TextMeshProUGUI SequenceText;

        [SerializeField] private string sequence01 = "Looks like a weapon on that table";
        [SerializeField] private float SequenceDelay = 1f;

        #endregion

        #region Unity Event Method
        private void OnTriggerEnter(Collider other)
        {
            //플레이어 체크
            if (other.tag == "Player")
            {
                //트리거 해제
                GetComponent<BoxCollider>().enabled = false;
                StartCoroutine(SequencePlayer());
            }
            
        }
        #endregion

        #region Custom Method
        IEnumerator SequencePlayer()
        {
            //플레이 캐릭터 비활성화 (플레이 멈춤)
            playerObject.SetActive(false);

            //대사 출력 : "Looks like a weapon on that table"
            SequenceText.text = sequence01;

            //1초 딜레이
            yield return new WaitForSeconds(SequenceDelay);

            //화살표 활성화
            Arrow.SetActive(true);

            //2초 딜레이
            yield return new WaitForSeconds(SequenceDelay*2f);

            //캐릭터 활성화
            playerObject.SetActive(true);

            //시나리오 텍스트 사라짐
            SequenceText.text = "";
        }
        #endregion
    }
}

