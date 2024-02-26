using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    public float bounceForce = 10f; // Force de rebond

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // R�cup�re le composant Rigidbody
        rb.velocity = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized * bounceForce; // Donne une v�locit� initiale � la sph�re
    }

    void Update()
    {
        // V�rifie si la sph�re sort du bas de l'�cran
        if (transform.position.y < -5f)
        {
            // Inverse la direction de la v�locit� en Y et applique la force de rebond
            rb.velocity = new Vector3(rb.velocity.x, bounceForce, rb.velocity.z);
        }
    }
}


