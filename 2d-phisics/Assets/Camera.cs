using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform player;

    void Update()
    {
        transform.localPosition = new Vector3(
            player.localPosition.x,
            player.localPosition.y,
            transform.localPosition.z
        );
    }
}
