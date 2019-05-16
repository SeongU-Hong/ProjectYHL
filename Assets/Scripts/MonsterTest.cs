using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTest : MonoBehaviour {

    public float nBackDist;
    public GameObject go;
    private Rigidbody rb;
    public float hitPower;
    private bool nBackFlag;
    private float spendTime;
    
    private void Start()
    {
        spendTime = 0f;
        rb = go.GetComponent<Rigidbody>();
        nBackFlag = false;
        //PlayerVector = js.GetPlayerVector();
    }

    private void Update()
    {
        if (spendTime > 50)
        {

        }
        else
        {
            spendTime += Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "bear_hand")
        {
            //rb.AddForce(Vector3.forward, ForceMode.VelocityChange);
            nBackFlag = true;
            go.transform.eulerAngles = JoyStickBear.bearVector;
            Debug.Log(go.transform.rotation);
            Debug.Log("닿았음");
            NBack();
        }
    }

    private void NBack()
    {
        go.transform.eulerAngles = JoyStickBear.bearVector;
        //this.transform.Translate(Vector3.forward*10f);
        rb.AddForce(rb.transform.rotation * Vector3.forward * 6f, ForceMode.VelocityChange);
    }

}
