using UnityEngine;

public class Star : MonoBehaviour
{
    public bool collected = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out _))
        {
            transform.localPosition = new Vector3(-1000f, -1000f, 0f);
            collected = true;
        }
    }
}
