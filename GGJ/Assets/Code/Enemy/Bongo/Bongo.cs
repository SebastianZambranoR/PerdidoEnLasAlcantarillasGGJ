using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bongo : MonoBehaviour
{
    [SerializeField] EnemyBasicPatrol patrol;
    [SerializeField] BongoAttack attack;
    [SerializeField] LayerMask PlayerMask;

    Animator anim;

    [SerializeField] float attackCooldown;
    float cooldownTimer;

    bool playerInRange;
    [SerializeField] float detectionRange;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        attack.enabled = false;
        patrol.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange)
        {
            cooldownTimer += Time.deltaTime;
            if (cooldownTimer > attackCooldown)
            {
                patrol.enabled = false;
                anim.Play("ATTACK");
                cooldownTimer = 0;
            }
        }

        DetectPlayer();
    }

    void InstantiateBubble()
    {
        attack.enabled = true;
    }

    void ContinuePatrol()
    {
        patrol.enabled = true;
        attack.enabled = false;
        anim.Play("IDLE");
    }

    void DetectPlayer()
    {
        if (Physics2D.OverlapCircle(transform.position, detectionRange, PlayerMask))
        {
            playerInRange = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if (collision.collider.GetComponent<Player>().attackPerformed)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
