using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float gravityForce = 5f;
    public Vector2 initialImpulse = new Vector2(3f, 0);

    public float gravityMinDistance = 0.25f;
    public float gravityMaxDistance = 5f;

    private GravityPoints gravityPoints;
    private Rigidbody2D body;

    private void FixedUpdate()
    {
        var points = gravityPoints.GetPointsCoordinates();

        foreach (var point in points)
        {
            var direction = Vector3.Normalize(point - transform.localPosition);
            var distance = Vector3.Distance(point, transform.localPosition);

            if (Mathf.Abs(distance) >= gravityMaxDistance || Mathf.Abs(distance) <= gravityMinDistance)
            {
                continue;
            }

            var force = gravityForce / distance;
            var impulse = direction * force * Time.fixedDeltaTime;

            body.AddForce(impulse, ForceMode2D.Impulse);
        }
    }

    private void Awake()
    {
        gravityPoints = FindObjectOfType<GravityPoints>();
        body = GetComponent<Rigidbody2D>();

        body.AddForce(initialImpulse, ForceMode2D.Impulse);
    }
}
