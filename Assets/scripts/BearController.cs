using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearController : MonoBehaviour {

    public float speed=4f;
    public float rotateSpeed=4f;
    Rigidbody bearRigidbody;
    Vector3 movement;

    private void Awake()
    {
        bearRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        //리지드바디를 컨트롤할 때는 FixedUpdate에서 실행.
        move(h, v);
        turn(h, v);
    }

    void move(float h, float v)
    {
        movement.Set(h, 0, v);
        movement = movement.normalized * speed * Time.deltaTime;

        bearRigidbody.MovePosition(transform.position + movement);
    }

    void turn(float h, float v)
    {
        //원점으로 돌아가는 것 방지.
        if (h == 0 && v == 0)
        {
            return;
        }

        Quaternion newRotation = Quaternion.LookRotation(movement);

        bearRigidbody.rotation = Quaternion.Slerp(bearRigidbody.rotation, newRotation, rotateSpeed * Time.deltaTime);
    }
}
