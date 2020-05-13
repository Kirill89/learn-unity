using UnityEngine;

public class MouseCamLook : MonoBehaviour
{
    private Transform character;
    private float sensitivity = 4.0f;
    private float smoothing = 2.0f;
    private Vector2 desiredMouseLook;
    private Vector2 mouseLook;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        character = transform.parent;
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
        }

        desiredMouseLook += new Vector2(
            Input.GetAxisRaw("Mouse X") * sensitivity * smoothing,
            Input.GetAxisRaw("Mouse Y") * sensitivity * smoothing
        );
        mouseLook = new Vector2(
            Mathf.Lerp(mouseLook.x, desiredMouseLook.x, 1f / smoothing),
            Mathf.Lerp(mouseLook.y, desiredMouseLook.y, 1f / smoothing)
        );

        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        character.localRotation = Quaternion.AngleAxis(mouseLook.x, character.up);
    }
}
