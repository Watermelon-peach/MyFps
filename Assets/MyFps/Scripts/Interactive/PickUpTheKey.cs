using UnityEngine;

namespace MyFps
{
    public class PickUpTheKey : Interactive
    {
        #region Variables
        [SerializeField]
        private PuzzleKey puzzleKey = PuzzleKey.ROOM01_KEY;
        #endregion

        #region Custom Method
        protected override void DoAction()
        {
            //퍼즐 아이템 (key) 획득
            PlayerDataManager.Instance.AddPuzzleKey(puzzleKey);
            Debug.Log("퍼즐 아이템 (key) 획득");

            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
        #endregion

    }

}
