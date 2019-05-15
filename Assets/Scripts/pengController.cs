using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum Peng_State
{
    idle,
    trace,
    attacked,
    die
}


public class pengController : MonoBehaviour {
    NavMeshAgent agent;
    Transform endTr;
    Vector3 endPosition;
    Animator anime;

    bool check = true;

    public int peng_HP = 300;


	// Use this for initialization
	void Start () {
        anime = GetComponent<Animator>();

        agent = GetComponent<NavMeshAgent>();
        endPosition = new Vector3(81, 3, 27);

        endTr = this.gameObject.transform;
	}
	
	// Update is called once per frame
	void Update () {
     agent.SetDestination(endPosition);

	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "wolf_atk")
        {
            agent.Stop();
            Debug.Log("울프 어택");
            Debug.Log("체력 : " + peng_HP);
            peng_HP -= 25;
            if (check)
            {
                check = false;
                StartCoroutine(WaitForIt());
            }
        }

        //펭귄 사망
        if (peng_HP <= 0)
        {
            Debug.Log("펭귄 사망");
            agent.Stop();
            anime.SetTrigger("die");
            StopAllCoroutines();
            GetComponent<CapsuleCollider>().enabled = false;

        }
    }

    IEnumerator WaitForIt()
    {
        agent.Stop();
        yield return new WaitForSeconds(0.3f);
        check = true;
        agent.Resume();
    }
}
