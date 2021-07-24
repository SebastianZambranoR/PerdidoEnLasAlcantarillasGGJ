using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    [SerializeField]GameObject Door;
    SpriteRenderer sRenderer;

    [SerializeField] Color activateColor;
    private void Awake()
    {
        sRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.GetComponent<Player>().attackPerformed)
            {
                sRenderer.color = activateColor;
                Door.SetActive(false);
            }
        }
    }
}
