using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGizmos : MonoBehaviour {

    public Color c = Color.yellow;
    public float radius = 0.3f;


    private void OnDrawGizmos()
    {
        Gizmos.color = c;
        Gizmos.DrawSphere(transform.position,radius);
    }

}
