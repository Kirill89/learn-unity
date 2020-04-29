using UnityEngine;

public class Flag : MonoBehaviour
{
    public string nextScene = "";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out _))
        {
            StateManager.GetInstance().MarkLevelAsFinished();

            if (!nextScene.Equals(""))
            {
                Helpers.LoadLevel(nextScene);
            }
        }
    }
}
