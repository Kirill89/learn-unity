using UnityEngine;

public class RotationTransformation : Transformation
{
    public Vector3 rotation;

    public override Matrix4x4 Matrix
    {
        get
        {
            float radX = rotation.x * Mathf.Deg2Rad;
            float radY = rotation.y * Mathf.Deg2Rad;
            float radZ = rotation.z * Mathf.Deg2Rad;
            float sinX = Mathf.Sin(radX);
            float cosX = Mathf.Cos(radX);
            float sinY = Mathf.Sin(radY);
            float cosY = Mathf.Cos(radY);
            float sinZ = Mathf.Sin(radZ);
            float cosZ = Mathf.Cos(radZ);

            return new Matrix4x4(
                new Vector4(
                    cosY * cosZ,
                    cosX * sinZ + sinX * sinY * cosZ,
                    sinX * sinZ - cosX * sinY * cosZ,
                    0f
                ),
                new Vector4(
                    -cosY * sinZ,
                    cosX * cosZ - sinX * sinY * sinZ,
                    sinX * cosZ + cosX * sinY * sinZ,
                    0f
                ),
                new Vector4(
                    sinY,
                    -sinX * cosY,
                    cosX * cosY,
                    0f
                ),
                new Vector4(0f, 0f, 0f, 1f)
            );
        }
    }
}
