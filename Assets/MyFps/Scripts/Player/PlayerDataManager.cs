using UnityEngine;

namespace MyFps
{
    //장착 무기 타입 enum
    public enum WeaponType
    {
        None,
        Pistol,
    }

    //퍼즐 아이템 enum값 설정
    public enum PuzzleKey
    {
        ROOM01_KEY,
        MAX_KEY              //퍼즐 아이템 개수
    }

    //플레이어 데이터 관리 클래스 - 싱글톤(다음 씬에서 데이터 보존)
    public class PlayerDataManager : PersistanceSingleton<PlayerDataManager>
    {
        #region Variables
        private int ammoCount;

        private bool[] hasKeys;     //퍼즐 아이템 소지 여부 체크
        #endregion

        #region Property
        public WeaponType Weapon { get; set; }

        //탄환 개수 리턴하는 읽기 전용 프로퍼티
        public int AmmoCount => ammoCount;
        #endregion

        private void Start()
        {
            InitPlayerData();
        }

        //플레이 데이터 초기화
        private void InitPlayerData()
        {
            //플레이 데이터 초기화
            ammoCount = 0;
            Weapon = WeaponType.None;

            //퍼즐 아이템 개수만큼 불형 요소수 생성
            hasKeys = new bool[(int)PuzzleKey.MAX_KEY];
        }
        #region Custom Method
        //ammo 저축 함수
        public void AddAmmo(int amount)
        {
            ammoCount += amount;
        }

        //ammo 사용 함수
        public bool UseAmmo(int amount)
        {
            //소지 ammo 체크
            if (ammoCount < amount)
            {
                Debug.Log("Not enough Ammo");
                return false;
            }

            ammoCount -= amount;
            return true;
        }

        //퍼즐 아이템 획득 - 매개변수로 퍼즐키 타입 받는다
        public void AddPuzzleKey(PuzzleKey keyType)
        {
            hasKeys[(int)keyType] = true;
        }
        //퍼즐 아이템 소지 여부 체크 - 매개변수로 퍼즐키 타입 받는다
        public bool HasPuzzleKey(PuzzleKey keyType)
        {
            return hasKeys[(int)keyType];
        }
        #endregion
    }
}

