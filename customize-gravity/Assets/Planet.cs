using UnityEngine;

public class Planet : MonoBehaviour
{
    Rigidbody rigidBody;

    private void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        rigidBody.AddForce(new Vector3(x, y, 0f), ForceMode.Acceleration);
    }

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }
}
