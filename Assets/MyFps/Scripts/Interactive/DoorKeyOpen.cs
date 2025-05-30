using UnityEngine;
using System.Collections;
using TMPro;

namespace MyFps
{
    public class DoorKeyOpen : Interactive
    {
        #region Variables
        public Animator animator;

        //시나리오 텍스트
        [SerializeField]
        private string sequence = "Door is Locked. I need a Key";
        #endregion

        #region Unity Event Method
        public TextMeshProUGUI sequenceText;
        #endregion

        #region Custom Method
        protected override void DoAction()
        {
            //조건에 따라 문이 안열린다
            //문열기
            if (PlayerDataManager.Instance.HasPuzzleKey(PuzzleKey.ROOM01_KEY))
            {
                OpenDoor();
            }
            else
            {
                //문이 안열려여!
                StartCoroutine(LockedDoor());
                Debug.Log("키를 찾아라");
            }
        }

        private void OpenDoor()
        {
            //문열기 연출, Sfx
            GetComponent<BoxCollider>().enabled = false;
            animator.SetBool("IsOpen", true);

            AudioManager.Instance.Play("DoorBang");

        }

        IEnumerator LockedDoor()
        {
            //인터랙티브 기능 끄기
            uninteractive = true;

            sequenceText.text = "";
            AudioManager.Instance.Play("DoorLocked");

            yield return new WaitForSeconds(1f);
            sequenceText.text = sequence;

            yield return new WaitForSeconds(1f);
            sequenceText.text = "";

            //인터랙티브 기능 켜기
            uninteractive = false;
        }
        #endregion
    }

}
