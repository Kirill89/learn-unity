using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Grid : MonoBehaviour
{
    public int xSize;
    public int ySize;

    private Vector3[] vertices;
    private int[] triangles;
    private Mesh mesh;

    private void Awake()
    {
        StartCoroutine(Generate());
    }

    private IEnumerator Generate()
    {
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Procedural Grid";

        WaitForSeconds wait = new WaitForSeconds(0.01f);
        int i = 0;

        vertices = new Vector3[(xSize + 1) * (ySize + 1)];
        Vector2[] uv = new Vector2[vertices.Length];
        Vector4[] tangents = new Vector4[vertices.Length];

        Vector4 tangent = new Vector4(1f, 0f, 0f, -1f);

        for (int y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                uv[i] = new Vector2((float)x / xSize, (float)y / ySize);
                tangents[i] = tangent;
                vertices[i++] = new Vector3(x, y);
                yield return wait;
            }
        }

        mesh.vertices = vertices;

        i = 0;
        // Each square is two triangles.
        triangles = new int[xSize * ySize * 2 * 3];

        for (int y = 0; y < ySize; y++)
        {
            for (int x = 0; x < xSize; x++)
            {
                int zeroPointIndex = x + y * (xSize + 1);

                triangles[i++] = zeroPointIndex;
                triangles[i++] = zeroPointIndex + xSize + 1;
                triangles[i++] = zeroPointIndex + 1;

                yield return wait;
                mesh.triangles = triangles;

                triangles[i++] = zeroPointIndex + 1;
                triangles[i++] = zeroPointIndex + xSize + 1;
                triangles[i++] = zeroPointIndex + xSize + 2;

                yield return wait;
                mesh.triangles = triangles;
            }
        }

        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.uv = uv;
        mesh.tangents = tangents;
    }

    private void OnDrawGizmos()
    {
        if (vertices == null || triangles == null)
        {
            return;
        }

        Gizmos.color = Color.black;
        float size = 0.3f;

        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], size);
            size = Mathf.MoveTowards(size, 0.1f, 0.01f);
        }

        for (int i = 0; i < triangles.Length / 3; i++)
        {
            Gizmos.DrawLine(vertices[triangles[i*3]], vertices[triangles[i * 3 + 1]]);
            Gizmos.DrawLine(vertices[triangles[i * 3]], vertices[triangles[i * 3 + 2]]);
            Gizmos.DrawLine(vertices[triangles[i * 3 + 1]], vertices[triangles[i * 3 + 2]]);
        }
    }
}
