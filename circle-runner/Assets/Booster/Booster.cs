using UnityEngine;

public class Booster : MonoBehaviour
{
    public Vector3 boostForce = new Vector3(8f, 0f, 0f);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out var player))
        {
            player.GetComponent<Rigidbody2D>().AddForce(boostForce, ForceMode2D.Impulse);
        }
    }
}
