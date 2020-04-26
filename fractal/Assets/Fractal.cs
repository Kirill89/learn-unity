﻿using UnityEngine;
using System.Collections;

public class Fractal : MonoBehaviour
{
    public Mesh[] meshes;
    public Material material;
    public int maxDepth;
    public float childScale = 0.5f;
    public float spawnProbability = 0.8f;
    public float maxRotationSpeed = 45f;
    public float maxTwist = 20f;

    private int depth = 0;
    private float rotationSpeed;

    private static Vector3[] childDirections = {
        Vector3.up,
        Vector3.right,
        Vector3.left,
        Vector3.forward,
        Vector3.back,
    };

    private static Quaternion[] childOrientations = {
        Quaternion.identity,
        Quaternion.Euler(0f, 0f, -90f),
        Quaternion.Euler(0f, 0f, 90f),
        Quaternion.Euler(90f, 0f, 0f),
        Quaternion.Euler(-90f, 0f, 0f),
    };

    private Material[,] materials;

    private void InitializeMaterials()
    {
        materials = new Material[maxDepth + 1, 2];

        for (int i = 0; i < materials.GetLength(0); i++)
        {
            float t = (float)i / maxDepth;

            materials[i, 0] = new Material(material);
            materials[i, 0].color = Color.Lerp(Color.white, Color.yellow, t * t);

            materials[i, 1] = new Material(material);
            materials[i, 1].color = Color.Lerp(Color.white, Color.cyan, t * t);
        }

        materials[materials.GetLength(0) - 1, 0].color = Color.magenta;
        materials[materials.GetLength(0) - 1, 1].color = Color.red;
    }

    private void Start()
    {
        rotationSpeed = Random.Range(-maxRotationSpeed, maxRotationSpeed);
        transform.Rotate(Random.Range(-maxTwist, maxTwist), 0f, 0f);

        if (materials == null)
        {
            InitializeMaterials();
        }

        gameObject.AddComponent<MeshFilter>().mesh = meshes[Random.Range(0, meshes.Length)];
        gameObject.AddComponent<MeshRenderer>().material = material;

        GetComponent<MeshRenderer>().material = materials[depth, Random.Range(0, 2)];

        if (depth < maxDepth)
        {
            StartCoroutine(CreateChildren());
        }
    }

    private IEnumerator CreateChildren()
    {
        for (int i = 0; i < childDirections.Length; i++)
        {
            if (Random.value >= spawnProbability)
            {
                continue;
            }
            yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
            new GameObject("Fractal Child").AddComponent<Fractal>().Initialize(this, childDirections[i], childOrientations[i]);
        }
    }

    private void Initialize(Fractal parent, Vector3 dir, Quaternion orient)
    {
        material = parent.material;
        maxDepth = parent.maxDepth;
        depth = parent.depth + 1;
        childScale = parent.childScale;
        transform.parent = parent.transform;
        materials = parent.materials;
        meshes = parent.meshes;
        spawnProbability = parent.spawnProbability;
        maxRotationSpeed = parent.maxRotationSpeed;
        maxTwist = parent.maxTwist;

        // "localScale" is relative to parent.
        transform.localScale = Vector3.one * childScale;
        transform.localPosition = dir * (0.5f + 0.5f * childScale);
        transform.localRotation = orient;
    }

    private void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }
}