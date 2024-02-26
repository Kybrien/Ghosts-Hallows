using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    public float bounceForce = 10f; // Force de rebond

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Récupère le composant Rigidbody
        rb.velocity = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized * bounceForce; // Donne une vélocité initiale à la sphère
    }

    void Update()
    {
        // Vérifie si la sphère sort du bas de l'écran
        if (transform.position.y < -5f)
        {
            // Inverse la direction de la vélocité en Y et applique la force de rebond
            rb.velocity = new Vector3(rb.velocity.x, bounceForce, rb.velocity.z);
        }
    }
}


