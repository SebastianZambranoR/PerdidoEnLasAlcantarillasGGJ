using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PezAguja : MonoBehaviour
{
    EnemyBasicPatrol patrol;
    PezAgujaAttack attack;
    GameObject player;
    Animator anim;
    Rigidbody2D rb;
    [SerializeField] float detectionRange;
    SpriteRenderer sRenderer;
    [SerializeField] LayerMask PlayerMask;

    bool playerInRange;
    bool attacking;

    private void Awake()
    {
        patrol = GetComponent<EnemyBasicPatrol>();
        attack = GetComponent<PezAgujaAttack>();
        sRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        patrol.enabled = true;
        attack.enabled = false;
        anim.Play("IDLE");
    }

    // Update is called once per frame
    void Update()
    {
        if (patrol.enabled == false && attack.enabled == false)
        {
            attack.player = null;
            playerInRange = false;
        }

        if(Physics2D.OverlapCircle(transform.position, detectionRange, PlayerMask) && !attacking)
        {
            player = Physics2D.OverlapCircle(transform.position, detectionRange, PlayerMask).gameObject;
            attack.player = player;
            playerInRange = true;
        }

        if (playerInRange)
        {
            patrol.enabled = false;
            anim.Play("ATTACK");
            attack.enabled = true;
            attacking = true;
        }

        Flip();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            patrol.enabled = true;
            attacking = false;
            rb.velocity = Vector2.zero;
            anim.Play("IDLE");
        }
        if (collision.CompareTag("Player") && !attacking)
        {
            if (collision.GetComponent<Player>().attackPerformed)
            {
                Destroy(gameObject);
            }
        }else if(collision.CompareTag("Player") && attacking)
        {
            collision.GetComponent<Player>().GetDamage();
        }
    }

    void Flip()
    {
        if (transform.localEulerAngles.z > 90 && transform.localEulerAngles.z < 270)
        {
            sRenderer.flipX = true;
            sRenderer.flipY = true;
        }
        else
        {
            sRenderer.flipX = true;
            sRenderer.flipY = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
