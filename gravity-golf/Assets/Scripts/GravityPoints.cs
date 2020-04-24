using System.Collections.Generic;
using UnityEngine;

public class GravityPoints : MonoBehaviour
{
    public Transform gravityPointPrefab;

    List<Transform> gravityPoints = new List<Transform>();

    private Transform GetClicked(Vector2 clickPos)
    {
        var hitColliders = Physics2D.OverlapPointAll(clickPos);

        foreach (var hitCollider in hitColliders)
        {
            if (gravityPoints.Contains(hitCollider.transform))
            {
                return hitCollider.transform;
            }
        }

        return null;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var clickScreenPos = Input.mousePosition;
            var clickPos = Camera.main.ScreenToWorldPoint(clickScreenPos);
            var clickedPoint = GetClicked(clickPos);

            if (clickedPoint != null)
            {
                if (gravityPoints.Contains(clickedPoint))
                {
                    gravityPoints.Remove(clickedPoint);
                    Destroy(clickedPoint.gameObject);
                }
            }
            else
            {
                if (gravityPoints.Count < 10)
                {
                    var obj = Instantiate(gravityPointPrefab);
                    var body = obj.GetComponent<Rigidbody2D>();

                    Destroy(body);
                    obj.localPosition = new Vector3(clickPos.x, clickPos.y, 0f);
                    gravityPoints.Add(obj);
                }
            }
        }
    }

    public Vector3[] GetPointsCoordinates()
    {
        var points = new Vector3[gravityPoints.Count];

        for (int i = 0; i < points.Length; i++)
        {
            points[i] = gravityPoints[i].localPosition;
        }

        return points;
    }

    private void Start()
    {
        // Make gravity points "transparent" for other objects.
        int defaultLayer = LayerMask.NameToLayer("Default");
        int gravityPointsLayer = LayerMask.NameToLayer("GravityPoints");

        Physics2D.IgnoreLayerCollision(defaultLayer, gravityPointsLayer, true);
    }
}
