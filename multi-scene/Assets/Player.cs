using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int level;

    const float MAX_ANGULAR_VELOCITY = 270f; //  degrees per second.
    const float TORQUE_FORCE = 3f;
    const float JUMP_FORCE = 8f;

    const float MIN_Y = -10f;

    Rigidbody2D rigidBody;
    bool desiredJump = false;
    bool onGround = false;

    private void OnCollisionStay2D(Collision2D collision)
    {
        for (int i = 0; i < collision.contactCount; i++)
        {
            Vector2 normal = collision.GetContact(i).normal;

            if (normal.y >= 0.9f)
            {
                onGround = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Flag"))
        {
            SaveLoad.data.levelsDone = Math.Max(SaveLoad.data.levelsDone, level);
            SaveLoad.Save();

            SceneManager.LoadScene("Menu");
        }
    }

    private void Update()
    {
        desiredJump |= Input.GetButtonDown("Jump");
    }

    void FixedUpdate()
    {
        rigidBody.AddTorque(-Input.GetAxis("Horizontal") * TORQUE_FORCE);
        rigidBody.angularVelocity = Mathf.Clamp(rigidBody.angularVelocity, -MAX_ANGULAR_VELOCITY, MAX_ANGULAR_VELOCITY);

        if (desiredJump)
        {
            desiredJump = false;
            if (onGround)
            {
                rigidBody.AddForce(transform.up * JUMP_FORCE, ForceMode2D.Impulse);
            }
        }

        onGround = false;

        if (transform.localPosition.y < MIN_Y)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }
}
