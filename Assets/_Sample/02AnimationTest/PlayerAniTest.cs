using UnityEngine;

namespace MySample
{
    public class PlayerAniTest : MonoBehaviour
    {
        #region Variables
        //참조
        private Animator animator;

        //이동
        private float moveSpeed = 5f;

        //인풋
        float moveX;
        float moveY;

        //애니메이션 파라미터
        [SerializeField]
        private string moveMode = "MoveMode";
        #endregion

        #region Unity Event Method
        private void Start()
        {
            animator = GetComponent<Animator>();
        }
        private void Update()
        {
            //인풋
            moveX = Input.GetAxis("Horizontal");    //ad, 좌우 화살표
            moveY = Input.GetAxis("Vertical");      //ws, 위아래 화살표

            //방향
            Vector3 dir = new Vector3(moveX, 0f, moveY).normalized;
            transform.Translate(dir * Time.deltaTime * moveSpeed, Space.World);

            //애니메이션
            //AnimationStateTest();

            AnimationBlendTest();
            /*animator.SetInteger(moveMode, 0);
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.Self);
                animator.SetInteger(moveMode, 1);
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(Vector3.left * moveSpeed * Time.deltaTime, Space.Self);
                animator.SetInteger(moveMode, 4);
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(Vector3.back * moveSpeed * Time.deltaTime, Space.Self);
                animator.SetInteger(moveMode, 3);
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(Vector3.right * moveSpeed * Time.deltaTime, Space.Self);
                animator.SetInteger(moveMode, 2);
            }*/
        }
        #endregion

        #region Custom Method
        //
        private void AnimationBlendTest()
        {
            animator.SetFloat("MoveX", moveX);
            animator.SetFloat("MoveY", moveY);
        }
        //2. 앞뒤좌우 이동시 앞뒤좌우 애니메이션 플레이시켜준다
        //3. 이동이 없을 때에는 대기 애니메이션을 플레이한다
        private void AnimationStateTest()
        {
            if (moveX == 0f && moveY == 0f)
            {
                animator.SetInteger(moveMode, 0);
            }
            else
            {
                //앞뒤좌우
                if (moveY > 0)
                {
                    animator.SetInteger(moveMode, 1);
                }
                if (moveY < 0)
                {
                    animator.SetInteger(moveMode, 3);
                }
                if (moveX < 0)
                {
                    animator.SetInteger(moveMode, 4);
                }
                if (moveX > 0)
                {
                    animator.SetInteger(moveMode, 2);
                }
            }
        }
        #endregion
    }

}

/*
1. WASD 입력 받아 플레이어 앞뒤좌우 이동하고 (old input)
2. 앞뒤좌우 이동시 앞뒤좌우 애니메이션 플레이시켜준다
3. 이동이 없을 때에는 대기 애니메이션을 플레이한다
4. 이동속도 5

 */