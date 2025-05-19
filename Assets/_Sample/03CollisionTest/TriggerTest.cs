using UnityEngine;

public class TriggerTest : MonoBehaviour
{
    #region
    private Color startMaterialColor;
    [SerializeField] private float force = 0f;
    #endregion

    #region Unity Event Method
    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            Debug.Log($"OnTriggerEnter : {other.transform.tag.ToString()}");
            //Material 바꾸기
            startMaterialColor = other.transform.GetComponent<Renderer>().material.color;
            other.transform.GetComponent<Renderer>().material.color = Color.red;

            Rigidbody rb = other.transform.GetComponent<Rigidbody>();
            rb.AddForce(Vector3.right * force, ForceMode.Impulse);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other != null)
        {
            Debug.Log($"OnTriggerStay : {other.transform.tag.ToString()}");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other != null)
        {
            Debug.Log($"OnTriggerExit : {other.transform.tag.ToString()}");
            other.transform.GetComponent<Renderer>().material.color = startMaterialColor;
            Rigidbody rb = other.transform.GetComponent<Rigidbody>();
            rb.AddForce(Vector3.right * force, ForceMode.Impulse);
        }
    }
    #endregion
}
