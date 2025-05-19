using UnityEngine;

namespace MySample
{
    public class CollisionTest : MonoBehaviour
    {
        #region Variables
        private Color startMaterialColor;
        [SerializeField] private float force = 0f;
        #endregion
        #region Unity Event Method
        private void OnCollisionEnter(Collision collision)
        {
            if (collision != null)
            {
                //Debug.Log($"OnCollisionEnter : {collision.transform.tag.ToString()}");
                startMaterialColor = collision.transform.GetComponent<Renderer>().material.color;
                collision.transform.GetComponent<Renderer>().material.color = Color.red;

                Rigidbody rb = collision.transform.GetComponent<Rigidbody>();
                rb.AddForce(Vector3.left * force, ForceMode.Impulse);
            }
            
        }

        private void OnCollisionStay(Collision collision)
        {
            if (collision != null)
            {
                Debug.Log($"OnCollisionStay : {collision.transform.tag.ToString()}");
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision != null)
            {
                //Debug.Log($"OnCollisionExit : {collision.transform.tag.ToString()}");
                collision.transform.GetComponent<Renderer>().material.color = startMaterialColor;
                Rigidbody rb = collision.transform.GetComponent<Rigidbody>();
                rb.AddForce(Vector3.left * force, ForceMode.Impulse);
            }
        }
        #endregion
    }

}
