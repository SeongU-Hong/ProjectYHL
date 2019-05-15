using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackButton : MonoBehaviour {

    public GameObject player;
    GameObject bearHand;
    private Animator animator;
    public float coolTime;
    private float spendTime;
    private bool attackFlag;

    public float attackLength=0.66f;

    private void Start()
    {
        bearHand = GameObject.FindGameObjectWithTag("bear_hand");
        bearHand.GetComponent<Collider>().enabled = false;
        animator = player.GetComponent<Animator>();
        attackFlag = true;
    }

    public void Update()
    {
        if (spendTime >= coolTime)
        {
            attackFlag = true;
            GetComponent<Button>().interactable = true;
        }
        else
        {
            spendTime += Time.deltaTime;
        }

        if(spendTime> attackLength)
        {
           bearHand.GetComponent<Collider>().enabled = false;
        }
    }

    public void Attack()
    {
        bearHand.GetComponent<Collider>().enabled = true;
        animator.Play("bear_attack");
        GetComponent<Button>().interactable = false;
        spendTime = 0f;
    }
}
