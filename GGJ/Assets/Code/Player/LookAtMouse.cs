using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    Camera mainCamera;
    
    Vector2 MousePosition;
    Player player;

    [HideInInspector]public Vector2 startPosition;


    [SerializeField] GameObject chargingArrowPrefab;
    [SerializeField] float holdMaxTime;
    float holdTimer;

    bool attackPerform;

    GameObject chargingArrowInstance;
    public float offsetx;


    public delegate void CancelAttackHandler();
    public event CancelAttackHandler AttackCanceled;
    public delegate void AttackPerformHandler();
    public event AttackPerformHandler AttackPerformed;

    int finalFacingDirection;

    private void Awake()
    {
        mainCamera = Camera.main;
        player = GetComponent<Player>();

    }
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.right;   
    }

    // Update is called once per frame
    void Update()
    {
        MousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        Vector2 lookAtDirection = MousePosition - (Vector2)transform.position;

        transform.right = lookAtDirection;

        AttackManager();
        flipingManager();

    }

    void flipingManager()
    {
        if(transform.eulerAngles.z >= 90 && transform.eulerAngles.z <= 270)
        {
            transform.localScale = new Vector3(1, -1, 1);
            finalFacingDirection = -1;
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
            finalFacingDirection = 1;
        }
    }

    void AttackManager()
    {
        if (!attackPerform)
        {
            holdTimer += Time.deltaTime;
            if (holdTimer > holdMaxTime)
            {
                AttackCanceled.Invoke();
            }
        }
        

        if (Input.GetButtonUp("Fire1"))
        {
            attackPerform = true;
            AttackPerformed.Invoke();
            
        }
    }

    public float GetForceMultiplier()
    {
        float multiplier = 1;
        if(holdTimer <= 1)
        {
            multiplier = 1;
        }else if(holdTimer > 1 && holdTimer <= 2)
        {
            multiplier = 1.5f;
        }else if(holdTimer >2 && holdTimer <= 3)
        {
            multiplier = 2f;
        }else if(holdTimer > 3)
        {
            multiplier = 2.5f;
        }
        return multiplier;
    }

    private void OnEnable()
    {
        holdTimer = 0;
        attackPerform = false;
        chargingArrowInstance = Instantiate(chargingArrowPrefab, (Vector2)transform.position + new Vector2(offsetx,0)*player.facingDirection, Quaternion.identity, this.transform);
    }

    private void OnDisable()
    {
        if (!attackPerform)
        {
            transform.right = startPosition;
        }
        Destroy(chargingArrowInstance);
        player.ChangeFacingDirection(finalFacingDirection);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere((Vector2)transform.position + new Vector2(offsetx, 0), 0.05f);
    }

}
