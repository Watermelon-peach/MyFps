using UnityEngine;

namespace MyFps
{
    //트리거에 들어가면 플레이어가 안전지역에 있다 저장
    public class SafeZoneInTrigger : MonoBehaviour
    {
        #region Variables
        //SafeZoneInTrigger 오브젝트
        public GameObject SafeZoneOutTrigger;
        #endregion

        #region Unity Event Method
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                PlayerController.safeZoneIn = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Player")
            {
                //SafeZoneInTrigger 활성화
                SafeZoneOutTrigger.SetActive(true);

                //SafeZoneOutTrigger 비활성화
                gameObject.SetActive(false);
            }
        }
        #endregion
    }

}
