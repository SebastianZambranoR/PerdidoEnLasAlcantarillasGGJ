using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimbionteAttack2 : MonoBehaviour
{
    Simbionte simbionte;

    [SerializeField] float waitTime;
    float waitTimer;

    bool attack1;
    bool attack2;
    bool attack3;

    public delegate void OnAttack2Finish();
    public event OnAttack2Finish attack2Finish;

    private void Awake()
    {
        simbionte = GetComponent<Simbionte>();
    }

    // Update is called once per frame
    void Update()
    {
        waitTimer += Time.deltaTime;
        if(waitTimer > waitTime && !attack1)
        {
            attack1 = true;
            simbionte.Teleport();
            waitTimer = 0;
        }else if(waitTimer > waitTime && attack1 && !attack2)
        {
            attack2 = true;
            simbionte.Teleport();
            waitTimer = 0;
        }else if (waitTimer > waitTime && attack1 && attack2 && !attack3)
        {
            attack3 = true;
            simbionte.Teleport();
            waitTimer = 0;
        }else if (waitTimer > waitTime && attack1 && attack2 && attack3)
        {
            attack2Finish.Invoke();
        }
    }

    private void OnDisable()
    {
        attack1 = false;
        attack2 = false;
        attack3 = false;
        waitTimer = 0;
    }
}
