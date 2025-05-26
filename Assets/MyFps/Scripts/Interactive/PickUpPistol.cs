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
        public GameObject ammoBox;
        public GameObject secondTrigger;

        public GameObject ammoUI;
        #endregion


        protected override void DoAction()
        {  
            //무기 획득, 충돌체 제거
            realPistol.SetActive(true);

            theArrow.SetActive(false);
            ammoUI.SetActive(true);

            secondTrigger.SetActive(true);
            ammoBox.SetActive(true);
            //무기 데이터
            PlayerDataManager.Instance.Weapon = WeaponType.Pistol;

            gameObject.SetActive(false);    //fake pistol 및 충돌체 제거
        }
    }
}
