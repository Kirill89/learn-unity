using UnityEngine;

public class Camera : MonoBehaviour
{
    private readonly Rect playerBounds = new Rect(-20f, -20f, 40f, 40f);
    private Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        transform.localPosition = new Vector3(
            player.transform.localPosition.x,
            player.transform.localPosition.y,
            transform.localPosition.z
        );

        HandlePlayerFall();
    }

    private void HandlePlayerFall()
    {
        if (!playerBounds.Contains(player.transform.localPosition))
        {
            Helpers.RestartLevel();
        }
    }
}
