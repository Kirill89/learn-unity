using UnityEngine;

public class StoneToTree : MonoBehaviour
{
    public SpriteRenderer tree;

    SpriteRenderer stone;
    bool collides = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collides = true;
        }
    }

    private void FixedUpdate()
    {
        if (collides)
        {
            Color treeColor = tree.color;
            Color stoneColor = stone.color;

            treeColor.a = Mathf.MoveTowards(treeColor.a, 1f, 0.01f);
            stoneColor.a = Mathf.MoveTowards(stoneColor.a, 0f, 0.01f);

            tree.color = treeColor;
            stone.color = stoneColor;
        }
    }

    private void Awake()
    {
        stone = GetComponent<SpriteRenderer>();
    }
}
// 0.86
// -3.31