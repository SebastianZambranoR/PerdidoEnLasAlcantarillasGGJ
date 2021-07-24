using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public GameObject simbionte;
    public GameObject TrapDoor;
    public GameObject Music;
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            simbionte.SetActive(true);
            TrapDoor.SetActive(true);
            Music.SetActive(true);
        }
    }
}
