using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    [SerializeField] float knokbackForce;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Rigidbody2D rb = collision.collider.GetComponent<Player>().KnockBack();

            rb.AddForce((rb.position - (Vector2)transform.position).normalized* knokbackForce, ForceMode2D.Impulse);
        }
    }


}
