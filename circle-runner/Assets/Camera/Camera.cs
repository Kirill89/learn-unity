using UnityEngine;

public class Camera : MonoBehaviour
{
    private Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        transform.localPosition = new Vector3(
            player.transform.localPosition.x,
            player.transform.localPosition.y,
            transform.localPosition.z
        );
    }
}
