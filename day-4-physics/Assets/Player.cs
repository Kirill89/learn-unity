using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private const float MAX_SPEED = 10f;
    private const float MAX_ACCELERATION = 10f;
    private const float MAX_AIR_ACCELERATION = 1f;
    private const float JUMP_HEIGHT = 2f;

    Rigidbody body;
    Vector3 desiredVelocity;
    bool desiredJump;

    bool onGround = false;

    void OnCollisionStay(Collision collision)
    {
        for (int i = 0; i < collision.contactCount; i++)
        {
            Vector3 normal = collision.GetContact(i).normal;

            if (normal.y >= 0.9f)
            {
                onGround = true;
            }
        }
    }

    void Update()
    {
        Vector2 playerInput;
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");
        playerInput = Vector2.ClampMagnitude(playerInput, 1f);

        desiredVelocity = new Vector3(playerInput.x, 0f, playerInput.y) * MAX_SPEED;
        desiredJump |= Input.GetButtonDown("Jump");
    }

    void FixedUpdate()
    {
        float maxSpeedChange = (onGround ? MAX_ACCELERATION : MAX_AIR_ACCELERATION) * Time.deltaTime;
        Vector3 velocity = body.velocity;

        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
        velocity.z = Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxSpeedChange);

        if (desiredJump)
        {
            desiredJump = false;
            if (onGround)
            {
                velocity.y += Mathf.Sqrt(-2f * Physics.gravity.y * JUMP_HEIGHT);
            }
        }

        body.velocity = velocity;
        onGround = false;
    }

    void Awake()
    {
        body = GetComponent<Rigidbody>();
    }
}
