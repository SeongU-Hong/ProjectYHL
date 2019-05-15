using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTest : MonoBehaviour {

    Vector3 nBack;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "bear_hand")
        {
            Debug.Log("닿았다");
        }
    }

}
