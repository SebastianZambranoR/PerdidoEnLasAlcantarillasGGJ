using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cocodrilo : MonoBehaviour
{

    [SerializeField] float detectionRange;
    [SerializeField] LayerMask PlayerMask;
    SpriteRenderer Colorsito;
    public float CurrentLife = 3;
    private float Contador = 0.3f;
    float Cuentador;
    bool A = false;

    [SerializeField] float cooldown;
    float cooldownTimer;

    SpriteRenderer sRenderer;
    Animator anim;

    EnemyBasicPatrol patrol;
    CocodriloAttack attack;

    bool playerInRange;
    bool attacking;
    bool coolDown;
    GameObject player;

    private void Awake()
    {
        Colorsito = GetComponent<SpriteRenderer>();
        patrol = GetComponent<EnemyBasicPatrol>();
        attack = GetComponent<CocodriloAttack>();
        sRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        attacking = attack.isAttacking;
        if (Physics2D.OverlapCircle(transform.position, detectionRange, PlayerMask))
        {
            playerInRange = true;
            player = Physics2D.OverlapCircle(transform.position, detectionRange, PlayerMask).gameObject;
            attack.player = player;
        }

        if (coolDown)
        {
            cooldownTimer += Time.deltaTime;
            if(cooldownTimer > cooldown)
            {
                coolDown = false;
            }
        }

        if (A)
        {
            cambiarColor();
        }

        StateController();
        Flip();

        LifeManager();
    }

    void StateController()
    {
        if (!playerInRange)
        {
            patrol.enabled = true;
            attack.enabled = false;
        }
        else
        {
            if (!coolDown)
            {
                patrol.enabled = false;
                attack.enabled = true;
            }
            
        }
    }

    void CoolDown()
    {
        cooldownTimer = 0;
        coolDown = true;
        attack.enabled = false;
        anim.Play("IDLE");
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


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if (collision.collider.GetComponent<Player>().attackPerformed && !attack.isAttacking)
            {
                CurrentLife--;
                Cuentador = 0;
                A = true;
            }else 
            {
                collision.collider.GetComponent<Player>().GetDamage();
            }
                
        }
    }

    private void cambiarColor()
    {
        Colorsito.color = Color.red;
        Cuentador += Time.deltaTime;
        if (Cuentador > Contador)
        {
            Colorsito.color = Color.white;
            A = false;
        }

    }

    void LifeManager()
    {
        if(CurrentLife <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
