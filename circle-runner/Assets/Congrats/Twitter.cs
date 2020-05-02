using UnityEngine;

public class Twitter : MonoBehaviour
{
    public Star[] stars;

    private void FixedUpdate()
    {
        for (var i = 0; i < stars.Length; i++)
        {
            if (!stars[i].collected)
            {
                return;
            }
        }

        transform.localPosition = new Vector3(0f, -4f, 0f);
    }
}
