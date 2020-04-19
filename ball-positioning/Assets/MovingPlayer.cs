using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlayer : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)]
    float bounciness = 0.5f;

    [SerializeField, Range(0f, 100f)]
    float maxSpeed = 10f;

    [SerializeField, Range(0f, 100f)]
    float maxAcceleration = 10f;

    [SerializeField]
	Rect allowedArea = new Rect(-4.5f, -4.5f, 9f, 9f);

    Vector3 velocity;

    void Update()
    {
        Vector2 playerInput;

        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");
        Vector2.ClampMagnitude(playerInput, 1f);
        // playerInput.Normalize();

        Vector3 desiredVelocity = new Vector3(playerInput.x, 0f, playerInput.y) * maxSpeed;
        float maxSpeedChange = maxAcceleration * Time.deltaTime;

        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
        velocity.z = Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxSpeedChange);

        Vector3 displacement = velocity * Time.deltaTime;
        Vector3 newPosition = transform.localPosition + displacement;

        // newPosition.x = Mathf.Clamp(newPosition.x, allowedArea.xMin, allowedArea.xMax);
        // newPosition.z = Mathf.Clamp(newPosition.z, allowedArea.yMin, allowedArea.yMax);
        if (newPosition.x > allowedArea.xMax)
        {
            newPosition.x = allowedArea.xMax;
            velocity.x = velocity.x * -1 * bounciness;
        }
        else if (newPosition.x < allowedArea.xMin)
        {
            newPosition.x = allowedArea.xMin;
            velocity.x = velocity.x * -1 * bounciness;
        }

        if (newPosition.z > allowedArea.yMax)
        {
            newPosition.z = allowedArea.yMax;
            velocity.z = velocity.z * -1 * bounciness;
        }
        else if (newPosition.z < allowedArea.yMin)
        {
            newPosition.z = allowedArea.yMin;
            velocity.z = velocity.z * -1 * bounciness;
        }

        transform.localPosition = newPosition;
    }
}
