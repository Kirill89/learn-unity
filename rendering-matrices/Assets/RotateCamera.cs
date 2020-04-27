using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public float cameraSpeed = 4f;
    public Transform target;

    private void Start()
    {
        transform.LookAt(target);
    }

    void Update()
    {
        transform.Translate(Vector3.right * cameraSpeed * Time.deltaTime);
        transform.LookAt(target);
    }
}
