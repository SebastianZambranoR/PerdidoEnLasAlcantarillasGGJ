using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWaterMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Player player;
    
    
    [SerializeField] private float Speed;
    [SerializeField] private float limitSpeed;
    [SerializeField] private float waterFric;




    float horizontalMovement;
    float verticalMovement;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
    }


    // Update is called once per frame
    void Update()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        
    }

    private void FixedUpdate()
    {
        MovementInput();
        WaterFriccion();
        LimitSpeed();
    }

    void MovementInput()
    {
        if (horizontalMovement != 0)
        {
            player.facingDirection = (int)horizontalMovement;
            player.Flip();
            HorizontalMovement();
        }
            

        if (verticalMovement != 0)
            VerticallMovement();
    }

    void HorizontalMovement()
    {
        rb.AddForce(Vector2.right * horizontalMovement * Speed);
    }
    void VerticallMovement()
    {
        rb.AddForce(Vector2.up * verticalMovement * Speed);
    }

    void LimitSpeed()
    {
        if (Mathf.Abs(rb.velocity.x) > limitSpeed)
            rb.velocity = new Vector2(limitSpeed*horizontalMovement, rb.velocity.y);

        if (Mathf.Abs(rb.velocity.y) > limitSpeed)
            rb.velocity = new Vector2(rb.velocity.x, limitSpeed*verticalMovement);
    }


    void WaterFriccion()
    {
        if(horizontalMovement == 0)
        {
            rb.velocity = new Vector2(rb.velocity.x / waterFric, rb.velocity.y); 
        }

        if (verticalMovement == 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y / waterFric);
        }
    }

}
