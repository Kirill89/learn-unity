using UnityEngine;

public class Sphere : MonoBehaviour
{
    public Transform planet;

    static readonly float G = 6.67408f * Mathf.Pow(10f, -11f);

    Rigidbody rigidBody;

    private float GetForce()
    {
        float dist = Vector3.Distance(planet.localPosition, transform.localPosition);
        float mass = 100000000000f;

        return G * (mass / dist);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(Time.time); // 0.72
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 point = ray.origin + (ray.direction * Vector3.Distance(ray.origin, Vector3.zero));

            planet.localPosition = new Vector3(point.x, point.y, 0f);
        }
    }

    private void FixedUpdate()
    {
        Vector3 dir = Vector3.Normalize(planet.localPosition - transform.localPosition);
        Vector3 force = dir * GetForce() * Time.fixedDeltaTime;

        Debug.Log(Vector3.Magnitude(force));
        if (Vector3.Magnitude(force) >= 0.02f)
        {
            rigidBody.AddForce(force, ForceMode.Impulse);
        }
    }

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();

        rigidBody.AddForce(Vector3.up * 3, ForceMode.Impulse);
    }
}
