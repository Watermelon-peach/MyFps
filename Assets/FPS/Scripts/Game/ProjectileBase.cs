using UnityEngine;
using UnityEngine.Events;

namespace Unity.FPS.Game
{
    //Projectile 클래스들의 부모클래스(추상 클래스)
    public abstract class ProjectileBase : MonoBehaviour
    {
        #region Variables
        //발사 시 등록 된 함수들을 호출하는 이벤트 함수
        public UnityAction OnShoot;
        #endregion

        #region Property
        public GameObject Owner { get; private set; }   //발사한 무기의 주인
        public Vector3 InitialPosition { get; private set; }    //발사할 때의 초기 위치
        public Vector3 InitialDirection { get; private set; }   //발사할 때의 초기 앞 방향
        public Vector3 InheritedMuzzleVelocity { get; private set; }    //발사 시 총구의 이동 속도
        public float InitialCharge { get; private set; }        //차지 타입 무기의 충전량
        #endregion

        #region Custom Method
        //매개변수로 발사체를 발사하는 무기를 받아온다
        public void Shoot(WeaponController controller)
        {
            Owner = controller.Owner;
            InitialPosition = transform.position;
            InitialDirection = transform.forward;
            InheritedMuzzleVelocity = controller.MuzzleWorldVelocity;
            InitialCharge = controller.CurrentCharge;

            //발사 시 등록된 함수들을 호출
            OnShoot?.Invoke();
        }
        #endregion
    }

}
