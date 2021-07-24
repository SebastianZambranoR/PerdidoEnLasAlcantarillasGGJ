using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BongoAttack : MonoBehaviour
{
    [SerializeField] GameObject bubbleSpawnPoint;
    [SerializeField] GameObject bubble;

    private void OnEnable()
    {
        GameObject instance = Instantiate(bubble, bubbleSpawnPoint.transform.position, Quaternion.identity);
        instance.GetComponent<BongoBubble>().upTime = GetRandom();
    }

    float GetRandom()
    {
        return Random.Range(0.1f, 1.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
