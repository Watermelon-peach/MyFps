using TMPro;
using UnityEngine;

namespace MyFps
{
    //아이템(권총) 획득 인터랙티브 구현
    public class PickUpPistol : Interactive
    {
        #region Variables
        //인터랙티브 액션 연출
        public GameObject realPistol;
        public GameObject theArrow;
        #endregion

        #region Unity Event Method
        private void Update()
        {
            //문과 플레이어와의 거리
            theDistance = PlayerCasting.distanceFromTarget;
        }
        #endregion

        protected override void DoAction()
        {
            realPistol.SetActive(true);
            theArrow.SetActive(false);

            gameObject.SetActive(false);
        }
    }
}
