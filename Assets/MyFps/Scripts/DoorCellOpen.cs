using UnityEngine;
using TMPro;

namespace MyFps
{
    //문열기 인터랙티브 액션 구현
    public class DoorCellOpen : MonoBehaviour
    {
        #region Variables
        //문과 플레이어와의 거리
        private float theDistance;

        //액션 UI
        public GameObject ActionUI;
        public TextMeshProUGUI actionText;

        [SerializeField]
        private string action = "Open The Door";

        //애니메이션
        public Animator animator;

        //애니메이션 파라미터 스트링
        private string paramIsOpen = "IsOpen";
        #endregion

        #region UnityEvent Method
        private void Update()
        {
            //문과 플레이어와의 거리
            theDistance = PlayerCasting.distanceFromTarget;
        }
        private void OnMouseOver()
        {
            actionText.text = action;

            if (theDistance <= 2f)
            {
                ShowActionUI();

                //키입력 체크
                if (Input.GetKeyDown(KeyCode.E))
                {
                    //UI 숨기기, 문열고, 충돌체 제거
                    HideActionUI();

                    animator.SetBool(paramIsOpen, true);
                    GetComponent<BoxCollider>().enabled = false;
                }
            }
            else
            {
                HideActionUI();
                animator.SetBool(paramIsOpen, false);
            }
        }

        private void OnMouseExit()
        {
            HideActionUI();
        }
        #endregion

        #region Custom Method
        private void ShowActionUI()
        {
            ActionUI.SetActive(true);
            actionText.text = action;
        }

        //Action UI 숨기기
        private void HideActionUI()
        {
            ActionUI.SetActive(false);
            actionText.text = "";
        }
        #endregion
    }

}
