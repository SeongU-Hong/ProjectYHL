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
    Animator anime;

    //늑대 공격 데미지
    public int wolf_damage = 25;
    //늑대 체력
    int wolf_HP = 100;



    public State state = State.idle;
    public float traceDist = 0.0f;
    public float attackDist = 2.0f;
    bool isDie = false;




	// Use this for initialization
	void Start () {
        nvAgent = GetComponent<NavMeshAgent>();
        monsterTr = GetComponent<Transform>();
        playerTr = GameObject.FindGameObjectWithTag("penguin").transform;
        anime = GetComponent<Animator>();

        StartCoroutine(CheckMonsterState());
        StartCoroutine(MonsterAction());

	}
	
	// Update is called once per frame
	void Update () {
        //nvAgent.SetDestination(playerTr.position);
	}

    //몬스터가 공격 받을시 충돌처리
    //bearAtk(곰 공격)
    //butaAtk(돼지 공격)
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "bearAtk" 
            || collision.gameObject.tag == "butaAtk")
        {
            anime.SetTrigger("hit");
        }

        //몬스터 사망 처리
        if(wolf_HP <= 0)
        {
            MonsterDie();
        }
    }

    void MonsterDie()
    {
        StopAllCoroutines();
        nvAgent.Stop();
        anime.SetTrigger("die");
        GetComponent<CapsuleCollider>().enabled = false;
    }


    //펭귄과의 거리에 따른 state 처리
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

    //state에 따른 몬스터의 애니메이션과 이동 처리
    IEnumerator MonsterAction()
    {
        while (!isDie)
        {
            switch (state)
            {
                case State.idle:
                    nvAgent.Stop();
                    anime.SetBool("IsTrace", false);
                    break;
                case State.trace:
                    anime.SetBool("IsAttack", false);
                    nvAgent.SetDestination(playerTr.position);
                    nvAgent.Resume();
                    anime.SetBool("IsTrace", true);
                    break;
                case State.attack:
                    anime.SetBool("IsAttack", true);
                    nvAgent.Stop();
                    break;
                case State.die:
                    break;

            }
            yield return new WaitForSeconds(0.3f);
        }
    }

}
