using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Player player;

    [SerializeField] float Speed;
    [SerializeField] float airFric;
    [SerializeField] float limitSpeed;


    float horizontalMovement;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        //rb.gravityScale = 1;
        if(!player.attackPerformed)
             rb.velocity = rb.velocity / 10f;
    }

    private void OnDisable()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        MovementInput();
        LimitSpeed();
    }

    void MovementInput()
    {
        if (horizontalMovement != 0)
        {
            player.facingDirection = (int)horizontalMovement;
            player.Flip();
            Movement();
        }
            

    }

    void Movement()
    {
        rb.AddForce(Vector2.right * horizontalMovement * Speed);
    }


    void LimitSpeed()
    {
        if (Mathf.Abs(rb.velocity.x) > limitSpeed)
            rb.velocity = new Vector2(limitSpeed * horizontalMovement, rb.velocity.y);
    }
}
