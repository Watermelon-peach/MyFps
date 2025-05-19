using UnityEngine;

namespace MySample
{
    public class MoveObject : MonoBehaviour
    {
        #region Variables
        private Rigidbody rb;
        [SerializeField] private float force = 0f;
        #endregion

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }
        private void Start()
        {
            rb.AddForce(Vector3.right * force, ForceMode.Impulse);
        }
    }

}
