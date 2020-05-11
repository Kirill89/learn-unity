using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
[RequireComponent(typeof(Rigidbody), typeof(MeshCollider))]
public class Block : MonoBehaviour
{
    public const float width = 2.5f;
    public const float height = 1.5f;
    public const float length = 7.5f;
    public const float deformation = 0.05f;
    public const float weight = 1.05f;

    private Mesh mesh;

    private Vector3 DeformRandomly(Vector3 point)
    {
        return new Vector3(
            Random.Range(point.x - deformation, point.x + deformation),
            Random.Range(point.y - deformation, point.y + deformation),
            Random.Range(point.z - deformation, point.z + deformation)
        );
    }

    private void InitMesh()
    {
        if (mesh != null)
        {
            return;
        }

        mesh = new Mesh();
        mesh.name = "Jenga block";

        var vertices = new Vector3[] {
            DeformRandomly(new Vector3(0f, 0f, 0f)),
            DeformRandomly(new Vector3(0f, height, 0f)),
            DeformRandomly(new Vector3(width, height, 0f)),
            DeformRandomly(new Vector3(width, 0f, 0f)),
            DeformRandomly(new Vector3(0f, 0f, length)),
            DeformRandomly(new Vector3(0f, height, length)),
            DeformRandomly(new Vector3(width, height, length)),
            DeformRandomly(new Vector3(width, 0f, length)),
        };

        var triangles = new int[] {
            0, 1, 2,
            2, 3, 0,

            7, 6, 5,
            5, 4, 7,

            1, 5, 6,
            6, 2, 1,

            4, 0, 3,
            3 ,7, 4,

            0, 4, 5,
            5, 1, 0,

            2, 6, 7,
            7, 3, 2,
        };

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();

        var uvs = new Vector2[vertices.Length];

        for (var i = 0; i < vertices.Length; i++)
        {
            uvs[i] = new Vector2(vertices[i].x / mesh.bounds.size.x, vertices[i].z / mesh.bounds.size.z);
        }
        mesh.uv = uvs;

        GetComponent<MeshFilter>().mesh = mesh;

        var collider = GetComponent<MeshCollider>();

        collider.convex = true;
        collider.sharedMesh = mesh;

        var body = GetComponent<Rigidbody>();

        body.mass = weight;
    }

    private void Start()
    {
        InitMesh();
    }

    private void FixedUpdate()
    {
        var body = GetComponent<Rigidbody>();

        if (body.velocity.magnitude < 0.1f)
        {
            body.velocity = new Vector3(0f, 0f, 0f);
        }

        if (body.angularVelocity.magnitude < 0.1f)
        {
            body.angularVelocity = new Vector3(0f, 0f, 0f);
        }
    }
}
