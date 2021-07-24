using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimbionteAttack3 : MonoBehaviour
{
    Simbionte simbionte;

    [SerializeField] Transform attackPoint;

    [SerializeField] Transform[] inferiorPoints;
    [SerializeField] Transform[] superiorPoints;

    [SerializeField] float Force;

    [SerializeField] float waitTime;
    float waitTimer;

    bool superiorShoot;
    bool inferiorShoot;

    public delegate void OnAttack3Finish();
    public event OnAttack3Finish attack3Finish;

    private void Awake()
    {
        simbionte = GetComponent<Simbionte>();
    }

    private void OnEnable()
    {
        transform.position = attackPoint.position;
    }

    private void OnDisable()
    {
        superiorShoot = false;
        inferiorShoot = false;
        waitTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (!superiorShoot)
        {
            for (int i = 0; i < superiorPoints.Length; i++)
            {
                UpInstances(superiorPoints[i].transform);
            }
            superiorShoot = true;
        }
        else
        {
            waitTimer += Time.deltaTime;
        }

        if (!inferiorShoot && waitTimer > waitTime)
        {
            for (int i = 0; i < superiorPoints.Length; i++)
            {
                DownInstances(inferiorPoints[i].transform);
            }
            inferiorShoot = true;
            attack3Finish.Invoke();
        }
        
    }

    private void UpInstances(Transform summonPoint)
    {
        GameObject inst = Instantiate(simbionte.proyectile, summonPoint.transform.position, Quaternion.identity);
        inst.GetComponent<Proyectile>().rb.AddForce(Vector2.down * Force, ForceMode2D.Impulse);
    }

    private void DownInstances(Transform summonPoint)
    {
        GameObject inst = Instantiate(simbionte.proyectile, summonPoint.transform.position, Quaternion.identity);
        inst.GetComponent<Proyectile>().rb.AddForce(Vector2.up * Force, ForceMode2D.Impulse);
    }
}
