using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public enum State
{
    idle,
    trace,
    attack,
    die
}

public class MonsterController : MonoBehaviour {

    NavMeshAgent nvAgent;
    Transform playerTr;
    Transform monsterTr;
    Animator anim;

    public State state = State.idle;
    public float traceDist = 0.0f;
    public float attackDist = 2.0f;
    bool isDie = false;




	// Use this for initialization
	void Start () {
        nvAgent = GetComponent<NavMeshAgent>();
        monsterTr = GetComponent<Transform>();
        playerTr = GameObject.FindGameObjectWithTag("penguin").transform;

        anim = GetComponent<Animator>();


        StartCoroutine(CheckMonsterState());
        StartCoroutine(MonsterAction());

	}
	
	// Update is called once per frame
	void Update () {
        //nvAgent.SetDestination(playerTr.position);
	}

    IEnumerator CheckMonsterState()
    {
        while (!isDie)
        {
            float dist = Vector3.Distance(playerTr.position, monsterTr.position);
            if(dist <= attackDist)
            {
                state = State.attack;
            }else if(dist <= traceDist)
            {
                state = State.trace;
            }
            else
            {
                state = State.idle;
            }
            yield return new WaitForSeconds(0.3f);


        }
    }

    IEnumerator MonsterAction()
    {
        while (!isDie)
        {
            switch (state)
            {
                case State.idle:
                    nvAgent.Stop();
                    anim.SetBool("IsTrace", false);
                    break;
                case State.trace:
                    anim.SetBool("IsAttack", false);
                    nvAgent.SetDestination(playerTr.position);
                    nvAgent.Resume();
                    anim.SetBool("IsTrace", true);
                    break;
                case State.attack:
                    anim.SetBool("IsAttack", true);
                    nvAgent.Stop();
                    break;
                case State.die:
                    break;

            }
            yield return new WaitForSeconds(0.3f);
        }
    }

}
