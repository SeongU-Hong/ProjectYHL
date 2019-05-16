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

public class MonsterController : MonoBehaviour
{
    
    public Rigidbody rigid;


    NavMeshAgent nvAgent;
    Transform playerTr;
    Transform monsterTr;
    Animator anime;
    public float nBackSpeed;

    //늑대 공격 데미지
    public int wolf_damage = 25;
    //늑대 체력
    int wolf_HP = 100;



    public State state = State.idle;
    public float traceDist = 0.0f;
    public float attackDist = 2.0f;
    bool isDie = false;



    // Update is called once per frame
    void Update()
    {
      
    }

    void OnPlayerDie()
    {
        StopAllCoroutines();
        nvAgent.Stop();
        anime.SetTrigger("PlayerDie");
    }


  

    //몬스터가 공격 받을시 충돌처리
    //bearAtk(곰 공격)
    //butaAtk(돼지 공격)
    private void OnCollisionEnter(Collision collision)
    {

        //몬스터 사망 처리
        if (wolf_HP <= 0)
        {
            MonsterDie();
        }

        if (collision.gameObject.tag == "bear_hand")
        {
            Debug.Log("늑대맞음");
            anime.SetTrigger("hit");
        }
    }

    void MonsterDie()
    {
        StopAllCoroutines();
        nvAgent.Stop();
        anime.SetTrigger("die");
        GetComponent<CapsuleCollider>().enabled = false;
        Invoke("ReturnPooling", 2.0f);
    }

    void ReturnPooling()
    {
        wolf_HP = 100;
        state = State.idle;
        GetComponent<CapsuleCollider>().enabled = true;
        gameObject.SetActive(false);


    }

    void Awake()
    {
        nvAgent = GetComponent<NavMeshAgent>();
        monsterTr = GetComponent<Transform>();
        playerTr = GameObject.FindGameObjectWithTag("penguin").transform;
        anime = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();


    }
    private void OnEnable()
    {
        StartCoroutine(CheckMonsterState());
        StartCoroutine(MonsterAction());
        pengController.OnPlayerDie += this.OnPlayerDie;
    }

    private void OnDisable()
    {
        pengController.OnPlayerDie -= this.OnPlayerDie;
    }


    //펭귄과의 거리에 따른 state 처리
    IEnumerator CheckMonsterState()
    {
        while (!isDie)
        {
            float dist = Vector3.Distance(playerTr.position, monsterTr.position);
            if (dist <= attackDist)
            {
                state = State.attack;
            }
            else if (dist <= traceDist)
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