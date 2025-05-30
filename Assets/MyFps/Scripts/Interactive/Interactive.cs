using TMPro;
using UnityEngine;

namespace MyFps
{
    //인터랙티브 액션의 부모 클래스
    public class Interactive : MonoBehaviour
    {
        #region Variables
        //트리거 플레이어와의 거리
        protected float theDistance;

        //액션 UI
        public GameObject ActionUI;
        public TextMeshProUGUI actionText;

        //크로스헤어
        public GameObject extraCross;

        [SerializeField]
        protected string action = "Do Interactive Action";

        [SerializeField]
        protected bool uninteractive = false;
        #endregion

        #region Unity Event Method
        private void Update()
        {
            //오브젝트와 플레이어와의 거리
            theDistance = PlayerCasting.distanceFromTarget;
        }

        private void OnMouseOver()
        {
            if (uninteractive)
                return;

            extraCross.SetActive(true);
            if (theDistance <= 2f)
            {
                ShowActionUI();

                //키입력 체크
                if (Input.GetKeyDown(KeyCode.E))
                {
                    //UI 숨기기
                    HideActionUI();
                    extraCross.SetActive(false);

                    DoAction();
                }
            }
            else
            {
                HideActionUI();
            }
        }

        private void OnMouseExit()
        {
            HideActionUI();
            extraCross.SetActive(false);
        }
        #endregion

        #region Custom Method
        //Action UI 보여주기
        protected void ShowActionUI()
        {
            ActionUI.SetActive(true);
            actionText.text = action;
        }

        //Action UI 숨기기
        protected void HideActionUI()
        {
            ActionUI.SetActive(false);
            actionText.text = "";
        }

        //액션 함수
        protected virtual void DoAction()
        {

        }
        #endregion
    }
}

