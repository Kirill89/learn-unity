using UnityEngine;

public class Player : MonoBehaviour
{
    const float MAX_SPEED = 10f;
    const float MAX_ACCELERATION = 20f;
    const float MAX_AIR_ACCELERATION = 5f;
    const float JUMP_SPEED = 4f;

    float desiredHorizontalVelocity;
    bool desiredJump = false;

    Rigidbody2D body;
    Animator animator;

    void Update()
    {
        desiredHorizontalVelocity = Input.GetAxis("Horizontal") * MAX_SPEED;
        desiredJump |= Input.GetButtonDown("Jump");

        animator.SetBool("Run", Mathf.Abs(body.velocity.x) > 0.001f);
        animator.SetBool("Ground", Mathf.Abs(body.velocity.y) < 0.001f);
    }

    private void FixedUpdate()
    {
        Vector3 velocity = body.velocity;
        bool onGround = Mathf.Abs(velocity.y) <= 0.001f;
        float maxSpeedChange = (onGround ? MAX_ACCELERATION : MAX_AIR_ACCELERATION) * Time.deltaTime;

        velocity.x = Mathf.MoveTowards(velocity.x, desiredHorizontalVelocity, maxSpeedChange);

        if (desiredJump)
        {
            desiredJump = false;

            if (onGround)
            {
                velocity.y = JUMP_SPEED;
            }
        }

        body.velocity = velocity;

        if (body.velocity.x > 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if(body.velocity.x < 0)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
}
