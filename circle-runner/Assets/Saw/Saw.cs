using UnityEngine;

public class Saw : MonoBehaviour
{
    private const float ROTATION_ANIMATION_SPEED = -90f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out _))
        {
            Helpers.RestartLevel();
        }
    }

    private void Update()
    {
        transform.Rotate(0f, 0f, ROTATION_ANIMATION_SPEED * Time.deltaTime);
    }
}
