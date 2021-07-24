using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerWaterMovement waterMovement;
    private PlayerAirMovement airMovement;
    private PlayerStats stats;
    private Animator anim;
    private LookAtMouse lookMouse;

    public int facingDirection;
    private Rigidbody2D rb;

    [SerializeField] float AttackTime;
    float attackTimer = 0;

    bool inWater;
    bool prepareAttack;
    bool blockAttackInput;
    bool attackFinish;
    [HideInInspector]public bool attackPerformed;
    bool knokbacking;

    float waterFric = 1.1f;

    [SerializeField] float AttackForce;
    [SerializeField] int AttackStaminaCost;

    private void Awake()
    {
        waterMovement = GetComponent<PlayerWaterMovement>();
        airMovement = GetComponent<PlayerAirMovement>();
        stats = GetComponent<PlayerStats>();
        lookMouse = GetComponent<LookAtMouse>();
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

    }
    // Start is called before the first frame update
    void Start()
    {
        MovemenUpdater(inWater);
        lookMouse.enabled = false;
        AttackCancel();
        blockAttackInput = false;
        facingDirection = 1;
    }
    private void OnEnable()
    {
        lookMouse.AttackCanceled += AttackCancel;
        lookMouse.AttackPerformed += AttackPerform;
        stats.IsDeath += Death;
    }

    private void OnDisable()
    {
        lookMouse.AttackCanceled -= AttackCancel;
        lookMouse.AttackPerformed -= AttackPerform;
        stats.IsDeath -= Death;
        waterMovement.enabled = false;
        airMovement.enabled = false;
        lookMouse.enabled = false;
        anim.Play("DEATH");
    }

    private void Update()
    {
        if (Input.GetButton("Fire1") && !blockAttackInput && CanAttack() && inWater)
        {
            anim.Play("ATTACKCHARGE");
            prepareAttack = true;
            MovemenUpdater(inWater);
        }
        else
        {
            prepareAttack = false;
        }
            
        if(Input.GetButtonUp("Fire1") && blockAttackInput && attackFinish)
        {
            blockAttackInput = false;
            MovemenUpdater(inWater);
        }

        if (!attackFinish && blockAttackInput)
        {
            AttackFinish();
            
        }

        
    }
    private void FixedUpdate()
    {
        if (prepareAttack)
            WaterFriccion();
        if(blockAttackInput)
            WaterFriccion();

        IsInWater();
    }

    void IsInWater()
    {
        if (inWater)
        {
            rb.gravityScale = 0;
        }
        else
        {
            rb.gravityScale = 1;
            asphyxiated();
        }
    }

    public void ChangeFacingDirection(int value)
    {
        facingDirection = value;
    }

    public void Flip()
    {
        if(facingDirection == 1)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void EnterInWater()
    {
        inWater = true;
        MovemenUpdater(inWater);
    }
    public void ExitWater()
    {
        inWater = false;
        if(attackFinish)
            MovemenUpdater(inWater);            
    }


    void MovemenUpdater(bool isInWater)
    {
        if (isInWater && !prepareAttack && !knokbacking)
        {
            waterMovement.enabled = true;
            airMovement.enabled = false;
            anim.Play("IDLE");
        }
        else if (!isInWater && !prepareAttack && !knokbacking)
        {
            waterMovement.enabled = false;
            anim.Play("OUTOFWATER");
            airMovement.enabled = true;
        }
        else if(!knokbacking)
        {
            waterMovement.enabled = false;
            airMovement.enabled = false;
            lookMouse.enabled = true;
        }
        else
        {
            waterMovement.enabled = false;
            airMovement.enabled = false;
            lookMouse.enabled = false;
        }
    }

    void WaterFriccion()
    {
            rb.velocity = new Vector2(rb.velocity.x / waterFric, rb.velocity.y / waterFric);
    }

    void AttackCancel()
    {
        blockAttackInput = true;
        lookMouse.enabled = false;
        attackFinish = true;
        anim.Play("IDLE");
    }
    
    void AttackPerform()
    {
        anim.Play("ATTACKRELEASE");
            stats.DecreaceStamina(30);
            lookMouse.enabled = false;
            blockAttackInput = true;
            attackPerformed = true;
            attackFinish = false;
            attackTimer = 0;
            rb.AddForce(transform.right * AttackForce * lookMouse.GetForceMultiplier(), ForceMode2D.Impulse);
    }

    private bool CanAttack()
    {
        if(stats.GetCurrentStamina() - AttackStaminaCost > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void AttackFinish()
    {
        
        attackTimer += Time.deltaTime;
        if (attackTimer >= AttackTime)
        {
            attackFinish = true;
            attackPerformed = false;
            blockAttackInput = false;
            Flip();
            MovemenUpdater(inWater);
            transform.right = lookMouse.startPosition;
        }
            

    }

    public void GetDamage()
    {
        stats.DecreaceHealt(20);
    }


    void asphyxiated()
    {
        stats.DecreaceOxygen(0.5f);
    }

    public Rigidbody2D KnockBack()
    {
        knokbacking = true;
        StartCoroutine(StopKnockback());
        return rb;
    }

    private IEnumerator StopKnockback()
    {
        yield return new WaitForSeconds(0.1f);
        knokbacking = false;
        StopCoroutine(StopKnockback());
    }

    void Death()
    {
        this.enabled = false;
    }
}
