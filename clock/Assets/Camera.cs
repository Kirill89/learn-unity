using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform position;

    const float Z_MIN = -20f;
    const float Z_MAX = -10f;
    const float STEP = 0.01f;

    private bool zoomOut = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (zoomOut) {
            position.localPosition = new Vector3(
                position.localPosition.x,
                position.localPosition.y,
                position.localPosition.z - STEP
            );
            if (position.localPosition.z <= Z_MIN) {
                zoomOut = !zoomOut;
            }
        } else {
            position.localPosition = new Vector3(
                position.localPosition.x,
                position.localPosition.y,
                position.localPosition.z + STEP
            );
            if (position.localPosition.z >= Z_MAX) {
                zoomOut = !zoomOut;
            }
        }

        // Debug.Log(position.localPosition);
    }
}
