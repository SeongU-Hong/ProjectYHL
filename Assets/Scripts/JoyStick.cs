using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour {

    public GameObject player;
    public GameObject stick; //조이스틱
    public float playerSpeed = 4f;

    private Vector3 stickFirstPosition; //조이스틱 처음 위치
    private Vector3 stickVector;    //조이스틱 벡터
    private float radius;   //조이스틱 배경의 반지름
    private bool moveFlag;
    private Animator animator;

    // Use this for initialization
    void Start ()
    {
        animator = player.GetComponent<Animator>();
        radius = GetComponent<RectTransform>().sizeDelta.y * 0.5f;
        stickFirstPosition = stick.transform.position;

        //캔버스 크기에 따른 반지름 조절
        float Can = transform.parent.GetComponent<RectTransform>().localScale.x;
        radius *= Can;

        moveFlag = false;
        animator.SetBool("isWalking", false);
    }

    void Update()
    {

        if (moveFlag)
        {
            player.transform.Translate(Vector3.forward * Time.deltaTime * playerSpeed);
            animator.SetBool("isWalking", true);
        }
        else
        {
        }
    }

    // 드래그
    public void Drag(BaseEventData _Data)
    {
        PointerEventData Data = _Data as PointerEventData;
        Vector3 Pos = Data.position;

        // 조이스틱을 이동시킬 방향을 구함.(오른쪽,왼쪽,위,아래)
        stickVector = (Pos - stickFirstPosition).normalized;

        // 조이스틱의 처음 위치와 현재 내가 터치하고있는 위치의 거리를 구한다.
        float Dis = Vector3.Distance(Pos, stickFirstPosition);

        // 거리가 반지름보다 작으면 조이스틱을 현재 터치하고 있는곳으로 이동. 
        if (Dis < radius)
            stick.transform.position = stickFirstPosition + stickVector * Dis;
        // 거리가 반지름보다 커지면 조이스틱을 반지름의 크기만큼만 이동.
        else
            stick.transform.position = stickFirstPosition + stickVector * radius;

        //조이스틱의 y값을 벡터로 변환
        Vector3 playerVector = new Vector3(0, Mathf.Atan2(stickVector.x, stickVector.y) * Mathf.Rad2Deg, 0);

        player.transform.eulerAngles = playerVector;

        moveFlag = true;

    }

    // 드래그 끝.
    public void DragEnd()
    {
        stick.transform.position = stickFirstPosition; // 스틱을 원래의 위치로.
        stickVector = Vector3.zero;          // 방향을 0으로.

        moveFlag = false;
        animator.SetBool("isWalking", false);
    }
}
