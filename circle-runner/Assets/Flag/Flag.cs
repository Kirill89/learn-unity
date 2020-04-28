using UnityEngine;
using UnityEngine.SceneManagement;

public class Flag : MonoBehaviour
{
    public string nextScene = "";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out _))
        {
            StateManager.GetInstance().MarkLevelAsFinished(SceneManager.GetActiveScene().name);
            if (!nextScene.Equals(""))
            {
                SceneManager.LoadScene(nextScene);
            }
        }
    }
}
