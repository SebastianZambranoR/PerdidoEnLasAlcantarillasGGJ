using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BongoBubble : MonoBehaviour
{
    public float lifeTime;
    float lifeTimer;
    public float upTime = 2;
    float upTimer;

    [SerializeField] float upSpeed;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        Up();
        DeathTime();
    }

    void Up()
    {
        upTimer += Time.deltaTime;
        if (upTimer < upTime)
        {
            rb.velocity = new Vector2(0, upSpeed);
        }else
        {
            rb.velocity = Vector2.zero;
        }
    }

    void DeathTime()
    {
        lifeTimer += Time.deltaTime;
        if(lifeTimer > lifeTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().GetDamage();
            Destroy(gameObject);
        }
    }
}
