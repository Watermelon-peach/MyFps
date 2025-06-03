using System.Collections;
using UnityEngine;
using TMPro;

namespace MyFps
{
    //퍼즐 조각을 모두 모으면 비밀의 문이 열린다
    public class FullEyeExit : Interactive
    {
        #region Variables
        public GameObject realPicture;
        public GameObject fakePicture;

        //숨겨진 벽 애니메이션
        public Animator hiddenWallAnimator;

        //메시지
        public TextMeshProUGUI sequenceText;
        [SerializeField] private string sequence = "Not Enough Pictures";
        #endregion

        #region Custom Method
        protected override void DoAction()
        {
            if (PlayerDataManager.Instance.HasPuzzleKey(PuzzleKey.LEFTEYE_KEY) && PlayerDataManager.Instance.HasPuzzleKey(PuzzleKey.RIGHTEYE_KEY))
            {
                fakePicture.SetActive(false);
                realPicture.SetActive(true);
                hiddenWallAnimator.SetTrigger("ExitOpen");

                uninteractive = true;
            }
            else
            {
                StartCoroutine(NotEnoughPictures());
            }
            
        }

        IEnumerator NotEnoughPictures()
        {
            uninteractive = true;
            sequenceText.text = sequence;
            yield return new WaitForSeconds(1.5f);

            uninteractive = false;
            sequenceText.text = "";
        }
        #endregion
    }
}

