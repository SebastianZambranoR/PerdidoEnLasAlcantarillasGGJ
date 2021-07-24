using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPortal : MonoBehaviour
{
    [SerializeField]Transform finalPosition;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.position = finalPosition.transform.position;
        }
    }
}
