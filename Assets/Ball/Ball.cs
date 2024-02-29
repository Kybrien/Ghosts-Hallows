using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoccerBall : MonoBehaviour
{
    public float kickForce = 7f; // Force du coup de pied

    private Rigidbody rb;

    void Start()
    {
        // R�cup�re le Rigidbody attach� � la balle
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            // Calcule la direction du coup de pied
            Vector3 kickDirection = (collision.transform.position - transform.position).normalized;

            // Applique la force du coup de pied � la balle
            rb.AddForce(kickDirection * kickForce, ForceMode.Impulse);
        }
    }
}
