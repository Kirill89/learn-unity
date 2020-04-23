using UnityEngine;
using System.Collections.Generic;

public class Controller : MonoBehaviour
{
    public Transform ball;
    public Transform gravityPointPrefab;

    public Rigidbody ballRigidbody;

    static readonly float G = 6.67408f * Mathf.Pow(10f, -11f);

    List<Transform> gravityPoints = new List<Transform>();

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && gravityPoints.Count < 10)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 point = ray.origin + (ray.direction * Vector3.Distance(ray.origin, Vector3.zero));
            Transform obj = Instantiate(gravityPointPrefab);

            obj.localPosition = new Vector3(point.x, point.y, 0f);

            gravityPoints.Add(obj);

        }
    }

    private void FixedUpdate()
    {
        foreach (var item in gravityPoints)
        {
            Vector3 direction = Vector3.Normalize(item.localPosition - ball.localPosition);
            float distance = Vector3.Distance(item.localPosition, ball.localPosition);
            float mass = 100000000000f;
            float force = G * (mass / distance);
            Vector3 impulse = direction * force * Time.fixedDeltaTime;

            if (Vector3.Magnitude(impulse) >= 0.01f)
            {
                ballRigidbody.AddForce(impulse, ForceMode.Impulse);
            }
        }
    }

    private void Awake()
    {
        ballRigidbody = ball.GetComponent<Rigidbody>();
    }
}
