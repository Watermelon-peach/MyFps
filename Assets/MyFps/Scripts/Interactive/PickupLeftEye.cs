using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

namespace MyFps
{
    //Left Eye 퍼즐 아이템 획득
    public class PickupLeftEye : Interactive
    {
        #region Variables
        //퍼즐 아이템
        [SerializeField]
        private PuzzleKey puzzleKey = PuzzleKey.LEFTEYE_KEY;

        //퍼즐 UI
        public GameObject puzzleUI;
        public Image puzzleImage;

        public Sprite puzzleSprite;

        //퍼즐 대사
        public TextMeshProUGUI sequenceText;
        [SerializeField]
        private string sequence = "You have obtained a puzzle item";
        #endregion

        #region Custom Method
        protected override void DoAction()
        {
            StartCoroutine(ShowPuzzleUI());
        }

        IEnumerator ShowPuzzleUI()
        {
            //Debug.Log("퍼즐 아이템 (LeftEye) 획득");
            PlayerDataManager.Instance.AddPuzzleKey(puzzleKey);

            //인터랙티브 기능 제거
            uninteractive = true;

            //연출
            sequenceText.text = "";

            puzzleUI.SetActive(true);
            puzzleImage.sprite = puzzleSprite;

            yield return new WaitForSeconds(0.5f);

            sequenceText.text = sequence;
            yield return new WaitForSeconds(1.7f);

            puzzleUI.SetActive(false);

            //값 초기화
            sequenceText.text = "";

            //트리거 오브젝트 킬
            Destroy(gameObject);
        }
        #endregion

    }

}
