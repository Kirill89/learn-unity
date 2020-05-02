using UnityEngine;

public class Player : MonoBehaviour
{
    public AudioClip hitSound;
    public AudioClip jumpSound;
    public float maxAngularVelocity = 270f;
    public float torqueForce = 3f;
    public float jumpForce = 8f;

    private const float MIN_GROUND_NORMAL = 0.9f;

    private bool onGround = false;
    private Rigidbody2D rigidBody;
    private bool inputJumpPressed = false;
    private float inputHorizontal = 0f;
    private AudioSource audioSource;

    private void DetectGround(Collision2D collision)
    {

        for (var i = 0; i < collision.contactCount; i++)
        {
            var normal = collision.GetContact(i).normal;

            if (Physics2D.gravity.normalized.Equals(Vector2.down) && normal.y >= MIN_GROUND_NORMAL)
            {
                onGround = true;
            }
            else if (Physics2D.gravity.normalized.Equals(Vector2.up) && -normal.y >= MIN_GROUND_NORMAL)
            {
                onGround = true;
            }
            else if (Physics2D.gravity.normalized.Equals(Vector2.right) && -normal.x >= MIN_GROUND_NORMAL)
            {
                onGround = true;
            }
            else if (Physics2D.gravity.normalized.Equals(Vector2.left) && normal.x >= MIN_GROUND_NORMAL)
            {
                onGround = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        audioSource.PlayOneShot(hitSound);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        DetectGround(collision);
    }

    private void HandleJump()
    {
        if (inputJumpPressed)
        {
            if (onGround)
            {
                rigidBody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
                audioSource.PlayOneShot(jumpSound);
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

    private void Update()
    {
        inputJumpPressed |= Input.GetButtonDown("Jump");
        inputHorizontal = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        HandleMove();
        HandleJump();
    }

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }
}
