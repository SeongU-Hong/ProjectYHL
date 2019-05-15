﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
    public float createTime = 3.0f;
    public GameObject monster;
    public Transform[] points;

    public List<GameObject> monsterPool = new List<GameObject>();
    public int maxPool = 4;


    public bool isGameOver = false;



	// Use this for initialization
	void Start () {

        points = GameObject.Find("SpawnPoints")
            .GetComponentsInChildren<Transform>();

        for(int i = 0; i < maxPool; i++)
        {
            GameObject _monster = Instantiate(monster);
            _monster.name = "Monster_" + i.ToString("00");
            _monster.SetActive(false);
            monsterPool.Add(_monster);

        }
	}

    IEnumerator CreateMonster()
    {
        while (!isGameOver)
        {
            yield return new WaitForSeconds(createTime);

            int idx = Random.Range(1, points.Length);
            GameObject _monster = Instantiate(monster);
            _monster.transform.position = points[idx].position;


        }
    }

	
	// Update is called once per frame
	void Update () {
		
	}
}
