using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoarAttackButton : MonoBehaviour
{

    public GameObject player;
    GameObject boarHead;
    public GameObject joyStickBoar;
    public GameObject joyStickFalse;
    private Animator animator;
    public float coolTime;
    private float spendTime;
    private bool attackFlag;
    private bool runFlag;
    public float runTimeLimit;
    private float runTime;
    public float runSpeed;

    private void Start()
    {
        boarHead = GameObject.FindGameObjectWithTag("boar_head");
        boarHead.GetComponent<Collider>().enabled = false;
        animator = player.GetComponent<Animator>();
        spendTime = 0f;
        runTime = 0f;
        runTimeLimit = 1.5f;
        runSpeed = 10f;
        attackFlag = true;
        runFlag = false;
    }

    public void Update()
    {

        //시간 지속 증가
        if (spendTime > 100f)
        {
        }
        else
        {
            spendTime += Time.deltaTime;
        }

        //쿨타임 풀기
        if (spendTime >= coolTime)
        {
            attackFlag = true;
            GetComponent<Button>().interactable = true;
        }

        //돌진시간만큼 돌진. 돌진 끝나면 콜리더 해제.
        if (runFlag)
        {
            BoarRun();
            runTime += Time.deltaTime;

            if (runTimeLimit < runTime)
            {
                animator.SetBool("isRunning", false);
                boarHead.GetComponent<Collider>().enabled = false;
                joyStickBoar.SetActive(true);
                joyStickFalse.SetActive(false);
                runFlag = false;
                runTime = 0;
            }
        }
    }

    //버튼 누르면 실행
    public void Attack()
    {
        boarHead.GetComponent<Collider>().enabled = true;
        animator.Play("boar_run");
        animator.SetBool("isRunning", true);
        GetComponent<Button>().interactable = false;
        player.transform.eulerAngles = JoyStickBoar.boarVector;
        joyStickBoar.SetActive(false);
        joyStickFalse.SetActive(true);
        Debug.Log(JoyStickBoar.boarVector);
        runFlag = true;
        spendTime = 0f;
    }

    //맷돼지 달리기
    public void BoarRun()
    {
        player.transform.Translate(Vector3.forward * runSpeed * Time.deltaTime);
    }
}
