using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cucaracha : MonoBehaviour
{
    Animator anim;
    EnemyBasicPatrol patrol;

    [SerializeField] float staticTime;
    float staticTimer;
    [SerializeField] float movingTime;
    float movingTimer;

    bool waiting;
   


    private void Awake()
    {
        anim = GetComponent<Animator>();
        patrol = GetComponent<EnemyBasicPatrol>();
    }
    // Start is called before the first frame update
    void Start()
    {
        patrol.enabled = false;
        waiting = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (waiting)
        {
            movingTimer = 0;
            staticTimer += Time.deltaTime;
            if(staticTimer > staticTime)
            {
                patrol.enabled = true;
                anim.Play("RUN");
                waiting = false;
            }
        }
        else
        {
            staticTimer = 0;
            movingTimer += Time.deltaTime;
            if(movingTimer > movingTime)
            {
                patrol.enabled = false;
                anim.Play("IDLE");
                waiting = true;
            }
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
            else
            {
                collision.collider.GetComponent<Player>().GetDamage();
            }
        }
    }
}
