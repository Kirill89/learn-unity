using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform levelBounds;

    private readonly Rect playerBounds = new Rect(-20f, -20f, 40f, 40f);
    private Player player;
    private UnityEngine.Camera gameCamera;
    private float levelWidth;
    private float levelHeight;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        gameCamera = GetComponent<UnityEngine.Camera>();

        levelWidth = levelBounds.localScale.x;
        levelHeight = levelBounds.localScale.y;
    }

    private void Update()
    {
        UpdateCameraPosition();
        HandlePlayerFall();
    }

    private void UpdateCameraPosition()
    {
        var camHeight = 2f * gameCamera.orthographicSize;
        var camWidth = camHeight * gameCamera.aspect;

        var distX = 0f;
        var distY = 0f;

        if (camWidth < levelWidth)
        {
            distX = levelWidth - camWidth;
        }

        if (camHeight < levelHeight)
        {
            distY = levelHeight - camHeight;
        }

        var camX = Mathf.Clamp(player.transform.localPosition.x, levelBounds.localPosition.x - distX, levelBounds.localPosition.x + distX);
        var camY = Mathf.Clamp(player.transform.localPosition.y, levelBounds.localPosition.y - distY, levelBounds.localPosition.y + distY);

        transform.localPosition = new Vector3(camX, camY, transform.localPosition.z);
    }

    private void HandlePlayerFall()
    {
        var violateMinX = player.transform.localPosition.x < levelBounds.localPosition.x - levelWidth / 2f;
        var violateMinY = player.transform.localPosition.y < levelBounds.localPosition.y - levelHeight / 2f;
        var violateMaxX = player.transform.localPosition.x > levelBounds.localPosition.x + levelWidth / 2f;
        var violateMaxY = player.transform.localPosition.y > levelBounds.localPosition.y + levelHeight / 2f;

        if (violateMinX || violateMinY || violateMaxX || violateMaxY)
        {
            Helpers.RestartLevel();
        }
    }
}
