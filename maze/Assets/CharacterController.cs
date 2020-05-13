using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private float speed = 10.0f;

    private void FixedUpdate()
    {
        var playerInput = new Vector2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal")) * speed * Time.fixedDeltaTime;

        Vector2.ClampMagnitude(playerInput, speed);
        transform.Translate(playerInput.y, 0, playerInput.x);
    }
}