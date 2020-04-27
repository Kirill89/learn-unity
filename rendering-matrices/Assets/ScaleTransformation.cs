using UnityEngine;

public class ScaleTransformation : Transformation
{
    public Vector3 scale = new Vector3(1f, 1f, 1f);

    public override Matrix4x4 Matrix
    {
        get
        {
            return new Matrix4x4(
                new Vector4(scale.x, 0f, 0f, 0f),
                new Vector4(0f, scale.y, 0f, 0f),
                new Vector4(0f, 0f, scale.z, 0f),
                new Vector4(0f, 0f, 0f, 1f)
            );
        }
    }
}
