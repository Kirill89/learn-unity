using UnityEngine;
using UnityEngine.SceneManagement;

public class Saw : MonoBehaviour
{
    private const float ROTATION_ANIMATION_SPEED = -90f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out _))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void Update()
    {
        transform.Rotate(0f, 0f, ROTATION_ANIMATION_SPEED * Time.deltaTime);
    }
}
