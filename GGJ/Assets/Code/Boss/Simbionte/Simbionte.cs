using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simbionte : MonoBehaviour
{
    SImbionteStats stats;
    SimbionteShootAttack shootAttack;
    public SImbionteTeleport teleport;
    SimbionteAttack2 attack2;
    SimbionteAttack3 attack3;
    [SerializeField] GameObject player;
    public GameObject proyectile;
    [SerializeField] GameObject proyectileLauncher;
    [SerializeField] float ProyectileForce;
    [SerializeField] int lifePoints;
    Animator anim;
    bool teleported;


    bool attacking;
    bool inCoolDown;

    [SerializeField] float cooldownTime;
    float cooldownTimer;

    [SerializeField] float fase1Duration;
    float fase1Timer;

    int Fase = 3;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        teleport = GetComponent<SImbionteTeleport>();
        attack2 = GetComponent<SimbionteAttack2>();
        attack3 = GetComponent<SimbionteAttack3>();
    }


    private void OnEnable()
    {
        teleport.teleported += PlayShow;
        attack2.attack2Finish += EndAttack2;
        attack3.attack3Finish += EndAttack3;
    }
    private void OnDisable()
    {
        teleport.teleported -= PlayShow;
        attack2.attack2Finish -= EndAttack2;
        attack3.attack3Finish -= EndAttack3;
    }

    void EndAttack2()
    {
        attacking = false;
        inCoolDown = true;
        attack2.enabled = false;
    }
    void EndAttack3()
    {
        attacking = false;
        inCoolDown = true;
        attack3.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        CoodownManager();
        AttackVariator();
        if (!inCoolDown)
            StateManager();
        Flip();

        if (Fase == 1)
            Fase1Controller();
    }

    void StateManager()
    {
        switch (Fase)
        {
            case 1:

                ConservateDistance();
                attack2.enabled = false;
                attack3.enabled = false;
                break;
            case 2:
                attack2.enabled = true;
                attack3.enabled = false;
                break;
            case 3:
                attack3.enabled = true;
                attack2.enabled = false;
                break;
        }
    }


    void ConservateDistance()
    {
        if(Vector2.Distance(transform.position, player.transform.position) < 5)
        {
            teleport.enabled = true;
            teleported = true;
            PlayBanish();
        }
    }

    void Fase1Controller()
    {
        fase1Timer += Time.deltaTime;
        if(fase1Timer > fase1Duration)
        {
            attacking = false;
            inCoolDown = true;
        }
    }

    void AttackVariator()
    {
        if(!inCoolDown && !attacking)
        {
            Fase = Random.Range(1, 4);
            attacking = true;
        }
    }

    void CoodownManager()
    {
        if (inCoolDown)
        {
            cooldownTimer += Time.deltaTime;
            if(cooldownTimer > cooldownTime)
            {
                inCoolDown = false;
                cooldownTimer = 0;
            }
        }
    }


    void Fase1Shooting()
    {
        GameObject instance = Instantiate(proyectile, proyectileLauncher.transform.position, Quaternion.identity);
        instance.GetComponent<Proyectile>().rb.AddForce((player.transform.position - transform.position).normalized * ProyectileForce, ForceMode2D.Impulse);
    }

    public void Teleport()
    {
        teleport.enabled = true;
        teleported = true;
        PlayBanish();
    }

    void Flip()
    {
        if((player.transform.position - transform.position).normalized.x> 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if (collision.collider.GetComponent<Player>().attackPerformed)
            {
                PlayTakeDamage();
                lifePoints--;
                if(lifePoints <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    void PlayBanish()
    {
        anim.Play("BANISH");
    }
    void PlayShow()
    {
        anim.Play("SHOW");
    }
    void PlayIdle()
    {
        anim.Play("IDLE");
    }
    void PlayAttack()
    {
        anim.Play("ATTACK");
    }
    void PlayTakeDamage()
    {
        anim.Play("TAKEDAMAGE");
    }

    void Shoot()
    {
        if (teleported)
        {
            Fase1Shooting();
            teleported = false;
        }
    }

}
