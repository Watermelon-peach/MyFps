using UnityEngine;

namespace MySample
{
    //플레이어를 offset 위치에서 쫓아간다
    public class CameraFollow : MonoBehaviour
    {
        #region Variables
        public Transform player;
        private Vector3 offset;

        #endregion

        #region Unity Event Method
        private void Start()
        {
            offset = player.position - transform.position;
        }

        private void LateUpdate()
        {
            transform.position = player.position - offset;
        }
        #endregion
    }

}
