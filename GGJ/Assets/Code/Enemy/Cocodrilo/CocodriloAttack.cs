using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CocodriloAttack : MonoBehaviour
{

    public GameObject player;
    [SerializeField] float Speed;
    Animator anim;

    public bool isAttacking;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        Movement();
    }

    void Movement()
    {
       
        if (Vector2.Distance(transform.position, player.transform.position) > 2.2 && !isAttacking)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, Speed * Time.deltaTime);
            Vector2 lookAtDirection = (Vector2)player.transform.position - (Vector2)transform.position;
            transform.right = lookAtDirection;
            anim.Play("IDLE");

        }
        else
        {           
                anim.Play("ATTACK");           
        }
    }

    void IsAttacking()
    {
        isAttacking = true; 
    }
    void NotIsAttacking()
    {
        isAttacking = false;
    }
}
