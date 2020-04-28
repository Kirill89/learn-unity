using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float maxAngularVelocity = 270f;
    public float torqueForce = 3f;
    public float jumpForce = 8f;
    public float minY = -10f;

    private bool onGround = false;
    private Rigidbody2D rigidBody;
    private bool inputJumpPressed = false;
    private float inputHorizontal = 0f;

    private void OnCollisionStay2D(Collision2D collision)
    {
        for (var i = 0; i < collision.contactCount; i++)
        {
            Vector2 normal = collision.GetContact(i).normal;

            if (normal.y >= 0.9f)
            {
                onGround = true;
            }
        }
    }

    private void HandleJump() {
        if (inputJumpPressed)
        {
            if (onGround)
            {
                rigidBody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            }

            inputJumpPressed = false;
        }

        onGround = false;
    }

    private void HandleMove()
    {
        rigidBody.AddTorque(-inputHorizontal * torqueForce);
        rigidBody.angularVelocity = Mathf.Clamp(rigidBody.angularVelocity, -maxAngularVelocity, maxAngularVelocity);
    }

    private void HandleFall()
    {
        if (transform.localPosition.y < minY)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void Update()
    {
        inputJumpPressed |= Input.GetButtonDown("Jump");
        inputHorizontal = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        HandleMove();
        HandleJump();
        HandleFall();
    }

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }
}
