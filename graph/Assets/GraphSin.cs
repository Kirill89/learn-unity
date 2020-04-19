using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphSin : Graph
{
    protected override void updateY(ref Vector3 position) {
        position.y = Mathf.Sin(Mathf.PI * (position.x + Time.time));
    }
}
