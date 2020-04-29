using UnityEngine;
using UnityEngine.SceneManagement;

public static class Helpers
{
    private static readonly Vector2 DEFAUKT_GRAVITY = new Vector2(0, -9.8f);

    public static void RestartLevel()
    {
        Physics2D.gravity = DEFAUKT_GRAVITY;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static void LoadLevel(string levelName)
    {
        Physics2D.gravity = DEFAUKT_GRAVITY;
        SceneManager.LoadScene(levelName);
    }
}
