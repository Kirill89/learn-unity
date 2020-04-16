using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    public Transform pointPrefab;

    Transform[] points;
    
    [Range(10, 100)]
    public int resolution = 10;

    protected virtual void updateY(ref Vector3 position) {
        position.y = Mathf.Sin(Mathf.PI * (position.x + Time.time));
    }

    void Update()
    {
        for (int i = 0; i < points.Length; i++)
        {
            Transform point = points[i];
            Vector3 position = point.localPosition;
            
            position.x = -1f + point.localScale.x * i;
            position.z = 0f;

            updateY(ref position);
            
            point.localPosition = position;
        }
    }

    void Awake()
    {
        points = new Transform[resolution];

        Vector3 scale = Vector3.one / resolution * 2;
        
        for (int i = 0; i < points.Length; i++)
        {
            Transform point = Instantiate(pointPrefab);
            
            point.localScale = scale;
            point.SetParent(transform, false);
            points[i] = point;
        }
    }
}
