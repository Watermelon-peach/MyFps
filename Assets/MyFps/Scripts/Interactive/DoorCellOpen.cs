using UnityEngine;
using TMPro;

namespace MyFps
{
    //문열기 인터랙티브 액션 구현
    public class DoorCellOpen : Interactive
    {
        #region Variables

        //애니메이션
        public Animator animator;

        //애니메이션 파라미터 스트링
        private string paramIsOpen = "IsOpen";
        #endregion

        #region Cutom Method
        protected override void DoAction()
        {
            animator.SetBool(paramIsOpen, true);
            GetComponent<BoxCollider>().enabled = false;
        }
        #endregion
    }

}
