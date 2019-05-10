using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCam : MonoBehaviour {

    //추적할 대상
    public GameObject player;
    //대상과의 거리
    public float dist = 7f;
    //카메라 높이
    public float height = 5f;

    public float followSpeed;

    Vector3 camPosition;

    void LateUpdate()
    {
        camPosition.x = player.transform.position.x;
        camPosition.z = player.transform.position.z - dist;
        camPosition.y = player.transform.position.y + height;

        //오브젝트의 transform은 내부적으로 선언되어있어서 따로 선언하지 않아도 사용가능.

        transform.position = Vector3.Lerp(transform.position, camPosition, followSpeed * Time.deltaTime);
    }
}
