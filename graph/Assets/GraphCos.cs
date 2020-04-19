using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphCos : Graph
{
    protected override void updateY(ref Vector3 position) {
        position.y = Mathf.Cos(Mathf.PI * (position.x + Time.time));
    }
}
