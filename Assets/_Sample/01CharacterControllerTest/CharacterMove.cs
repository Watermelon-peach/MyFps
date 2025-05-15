using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MySample
{
    //캐릭터 이동 관리 클래스
    public class CharacterMove : MonoBehaviour
    {
        #region Variables
        //참조
        private CharacterController controller;

        //입력
        private Vector2 inputMove;

        //이동
        [SerializeField]
        private float moveSpeed = 10f;

        //중력
        private float gravity = -9.81f;
        private Vector3 velocity;       //중력 계산에 의한 이동 속도

        //그라운드 체크
        public Transform groundCheck;   //발바닥 위치
        [SerializeField] private float checkRange = 0.2f;    //체크 하는 구의 반경
        [SerializeField] private LayerMask groundMask;      //그라운드 레이어 판별

        //점프 높이
        [SerializeField] float jumpHeight =1f;
        #endregion

        #region Unity Event Method
        private void Start()
        {
            //참조
            controller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            //땅에 있으면
            bool isGrounded = GroundCheck();
            if (isGrounded && velocity.y < 0f)
            {
                velocity.y = -5f;
            }
            //방향
            //Global 축 이동
            //Vector3 moveDir = Vector3.right * inputMove.x + Vector3.forward * inputMove.y;
            //Local 축 이동
            Vector3 moveDir = transform.right * inputMove.x + transform.forward * inputMove.y;

            //이동
            controller.Move(moveDir * Time.deltaTime * moveSpeed);

            //중력에 따른 이동
            velocity.y += 1.5f * gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);

        }
        #endregion

        #region Custom Method
        public void OnMove(InputAction.CallbackContext context)
        {
            inputMove = context.ReadValue<Vector2>();
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.started && GroundCheck())
            {
                //점프 높이만큼 뛰는 속도 구하기
                velocity.y = Mathf.Sqrt(-2f * gravity * jumpHeight);
            }
            
        }
        //그라운드 체크
        bool GroundCheck()
        {
            return Physics.CheckSphere(groundCheck.position, checkRange, groundMask);
        }
        #endregion
    }

}
