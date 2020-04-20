using UnityEngine;

public class ResetPlayer : MonoBehaviour
{
    public Vector3 startPosition;
    public float MIN_Y = -10f;


    private void FixedUpdate()
    {
        if (transform.localPosition.y < MIN_Y)
        {
            transform.localPosition = startPosition;
        }
    }
}
