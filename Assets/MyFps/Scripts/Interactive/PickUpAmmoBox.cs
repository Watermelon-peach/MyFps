using UnityEngine;

namespace MyFps
{
    public class PickUpAmmoBox : Interactive
    {
        #region Variables
        [SerializeField]
        private int giveAmmo = 7;
        #endregion
        #region Custom Method
        protected override void DoAction()
        {
            //Debug.Log("탄환 7개를 지급했습니다");
            PlayerDataManager.Instance.AddAmmo(giveAmmo);

            //이펙트 Vfx, Sfx

            //트리거 비활성화, 킬
            gameObject.SetActive(false);
            //Destroy(gameObject);
        }
        #endregion
    }

}
