using UnityEngine;

public class GravityDirection : MonoBehaviour
{
    public Vector2 gravity = new Vector2(0f, 9.8f);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out _))
        {
            Physics2D.gravity = gravity;
        }
    }
}
