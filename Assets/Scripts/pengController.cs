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
}
