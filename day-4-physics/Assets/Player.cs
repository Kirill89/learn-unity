using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private const float MAX_SPEED = 10f;
    private const float MAX_ACCELERATION = 10f;
    private const float MAX_AIR_ACCELERATION = 1f;
    private const float JUMP_HEIGHT = 2f;
    private readonly Vector3 INITIAL_POSITION = new Vector3(0f, 4f, 0f);
    private const float MIN_Y = -10f;

    Renderer renderer;
    Rigidbody body;
    Vector3 desiredVelocity;
    bool desiredJump;

    public Material endMaterial;

    bool onGround = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Target")
        {
            renderer.material = endMaterial;
        }
    }

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

        // Rotate input velocity by 45 degrees to make controls more intuitive.
        desiredVelocity = Quaternion.Euler(0, -45, 0) * desiredVelocity;

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

        if (transform.localPosition.y < MIN_Y)
        {
            transform.localPosition = INITIAL_POSITION;
            velocity = new Vector3(0f, 0f, 0f);
        }

        body.velocity = velocity;
        onGround = false;
    }

    void Awake()
    {
        body = GetComponent<Rigidbody>();
        renderer = GetComponent<Renderer>();
    }
}
