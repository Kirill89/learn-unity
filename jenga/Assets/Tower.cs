using System.Collections;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public Transform blockPrefab;
    public int height = 17;

    private Transform[] blocks;

    private void Start()
    {
        blocks = new Transform[height * 3];

        for (var i = 0; i < height; i++)
        {
            if (i % 2 == 0)
            {
                BuildVerticalLayer(i);
            }
            else
            {
                BuildHorizontalLayer(i);
            }
        }

        foreach (var block in blocks)
        {
            block.SetParent(transform, false);
        }

        StartCoroutine(Stabilize());
    }

    private IEnumerator Stabilize()
    {
        var wait = new WaitForSeconds(0.1f);

        foreach (var block in blocks)
        {
            var body = block.GetComponent<Rigidbody>();

            body.velocity = Vector3.zero;
            body.angularVelocity = Vector3.zero;
            yield return wait;
        }
    }

    private void BuildVerticalLayer(int layerIndex)
    {
        var y = (Block.height + Block.deformation) * layerIndex;

        var block = Instantiate(blockPrefab);
        block.localPosition = new Vector3(0f, y, 0f);
        blocks[3 * layerIndex + 0] = block;

        block = Instantiate(blockPrefab);
        block.localPosition = new Vector3(Block.width + Block.deformation, y, 0f);
        blocks[3 * layerIndex + 1] = block;

        block = Instantiate(blockPrefab);
        block.localPosition = new Vector3(-Block.width - Block.deformation, y, 0f);
        blocks[3 * layerIndex + 2] = block;
    }

    private void BuildHorizontalLayer(int layerIndex)
    {
        var rotation = Quaternion.Euler(0f, 90f, 0f);
        var y = (Block.height + Block.deformation) * layerIndex;

        var block = Instantiate(blockPrefab);
        block.localRotation = rotation;
        block.localPosition = new Vector3(-Block.width, y, Block.width * 3 + Block.deformation * 2);
        blocks[3 * layerIndex + 0] = block;

        block = Instantiate(blockPrefab);
        block.localRotation = rotation;
        block.localPosition = new Vector3(-Block.width, y, Block.width * 2 + Block.deformation);
        blocks[3 * layerIndex + 1] = block;

        block = Instantiate(blockPrefab);
        block.localRotation = rotation;
        block.localPosition = new Vector3(-Block.width, y, Block.width);
        blocks[3 * layerIndex + 2] = block;
    }
}
