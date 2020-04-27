using UnityEngine;

public class AnimateTransformations : MonoBehaviour
{
    private void FixedUpdate()
    {
        var rotationTransformation = GetComponent<RotationTransformation>();

        if (rotationTransformation != null)
        {
            rotationTransformation.rotation = new Vector3(
                rotationTransformation.rotation.x + 10f * Time.fixedDeltaTime,
                rotationTransformation.rotation.y + 20f * Time.fixedDeltaTime,
                rotationTransformation.rotation.z - 3f * Time.fixedDeltaTime
            );
        }

        var scaleTransformation = GetComponent<ScaleTransformation>();

        if (scaleTransformation != null)
        {
            var scale = Mathf.Lerp(0.5f, 3f, Mathf.PingPong(Time.time, 5) / 5f);

            scaleTransformation.scale = new Vector3(scale, scale, scale);
        }
    }
}
