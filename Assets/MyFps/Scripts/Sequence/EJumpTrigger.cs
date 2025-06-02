using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
namespace MyFps
{
    //날아가는 컵 연출 트리거
    public class EJumpTrigger : MonoBehaviour
    {
        #region Variables
        //플레이어 제어
        public PlayerInput playerInput;

        public GameObject activitySphere;
        #endregion

        #region Unity Event Method
        private void OnTriggerEnter(Collider other)
        {
            //트리거해제
            GetComponent<BoxCollider>().enabled = false;
            StartCoroutine(SequencePlayer());
        }
        #endregion

        #region Custom Method
        IEnumerator SequencePlayer()
        {
            playerInput.enabled = false;    //인풋 제어
            activitySphere.SetActive(true); //연출 시작

            yield return new WaitForSeconds(0.2f);
            activitySphere.SetActive(false);

            yield return new WaitForSeconds(2f);
            playerInput.enabled = true;
        }
        #endregion
    }

}
