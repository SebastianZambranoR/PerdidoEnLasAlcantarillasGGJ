using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PezAgujaAttack : MonoBehaviour
{
    public GameObject player;
    Rigidbody2D rb;
    Animator anim;


    [SerializeField] float chargingTime;
    float chargingTimer;

    [SerializeField] float attackForce;



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }


    // Update is called once per frame
    void Update()
    {
        LookAtPlayer();
        Attack();
    }

    private void OnDisable()
    {
        chargingTimer = 0;
    }

    void LookAtPlayer()
    {
        transform.right = player.transform.position - transform.position;
    }

    void Attack()
    {
        chargingTimer += Time.deltaTime;
        if(chargingTimer > chargingTime)
        {
            rb.AddForce((player.transform.position - transform.position).normalized * attackForce, ForceMode2D.Impulse);
            anim.Play("PERFORM");
            this.enabled = false;
        }
    }


}
