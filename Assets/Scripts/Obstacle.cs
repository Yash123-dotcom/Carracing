using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float speedreducefactor = 0.5f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("Car hit an obstacle!");
            Rigidbody2D carRb = collision.collider.GetComponent<Rigidbody2D>();
            if (carRb != null)
            {
                //reduces car velocity
                carRb.velocity *= speedreducefactor;
            }

        }
    }
}
