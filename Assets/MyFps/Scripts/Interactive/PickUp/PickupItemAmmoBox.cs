using UnityEngine;

namespace MyFps
{
    public class PickupItemAmmoBox : PickupItem
    {
        #region Variables
        //아이템 먹는 효과
        [SerializeField] private int giveAmmo = 7;
        #endregion

        #region Custom Mehtod
        protected override bool OnPickup()
        {
            PlayerDataManager.Instance.AddAmmo(giveAmmo);
            return true;
        }
        #endregion
    }

}
