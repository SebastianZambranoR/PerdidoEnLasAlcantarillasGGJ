using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicPatrol : MonoBehaviour
{
    SpriteRenderer sRenderer;
    
    [SerializeField] float waitTime;
    [SerializeField] float Speed;
    float waitTimer;
    public bool NeedFlipSystem;

    [SerializeField] float minX;
    [SerializeField] float maxX;
    [SerializeField] float minY;
    [SerializeField] float maxY;

    Vector2 target;


    private void Awake()
    {
        sRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        target = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        if (NeedFlipSystem)
            Flip();
    }
    void Movement()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, Speed * Time.deltaTime);
        Vector2 lookAtDirection = target - (Vector2)transform.position;
        transform.right = lookAtDirection;
        if(Vector2.Distance(transform.position, target) < 0.2f)
        {
            waitTimer += Time.deltaTime;
            if(waitTimer > waitTime)
            {
                target = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
                waitTimer = 0;
            }
        }
    }

    void Flip()
    {
        if(transform.localEulerAngles.z > 90 && transform.localEulerAngles.z < 270)
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

}
