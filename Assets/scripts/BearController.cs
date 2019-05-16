using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearController : MonoBehaviour {

    [SerializeField]
    private float speed = 4f;
    [SerializeField]
    private float rotateSpeed = 4f;

    Rigidbody bearRigidbody;
    Animator animator;

    private Vector3 movement;
    private float h;
    private float v;


<<<<<<< HEAD
    private void Awake()
=======
    private void Start()
>>>>>>> ProjectYHL/new_huyn
    {
        bearRigidbody = GetComponent<Rigidbody>();
        animator = GetComponent <Animator>();

    }

    private void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        AnimationUpdate();
    }

    private void FixedUpdate()
    {
        //리지드바디를 컨트롤할 때는 FixedUpdate에서 실행.
        Move();
        Turn();
    }

    void Move()
    {
        movement.Set(h, 0, v);
        movement = movement.normalized * speed * Time.deltaTime;

        bearRigidbody.MovePosition(transform.position + movement);
    }

    void Turn()
    {
        //원점으로 돌아가는 것 방지.
        if (h == 0 && v == 0)
        {
            return;
        }

        Quaternion newRotation = Quaternion.LookRotation(movement);

        bearRigidbody.rotation = Quaternion.Slerp(bearRigidbody.rotation, newRotation, rotateSpeed * Time.deltaTime);
    }

    void AnimationUpdate()
    {
        if (h == 0 && v == 0)
        {
            animator.SetBool("isWalking", false);
        }
        else
        {
            animator.SetBool("isWalking", true);
        }
    }
}
