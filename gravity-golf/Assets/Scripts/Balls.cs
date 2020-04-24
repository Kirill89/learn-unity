using System.Collections.Generic;
using UnityEngine;

public class Balls : MonoBehaviour
{
    public Transform ballPrefab;
    public int maxBalls = 10;

    List<Transform> balls = new List<Transform>();

    void Start()
    {
        InvokeRepeating("SpawnBall", 0f, 1.5f);
    }

    void SpawnBall() {
        var obj = Instantiate(ballPrefab);

        balls.Add(obj);

        if (balls.Count > maxBalls)
        {
            var objToDelete = balls[0];

            balls.Remove(objToDelete);
            Destroy(objToDelete.gameObject);
        }
    }
}
