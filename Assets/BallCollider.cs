using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollider : MonoBehaviour
{
    public float speed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float pushForce = 10f; // Force de pouss�e

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Rigidbody ballRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            if (ballRigidbody != null)
            {
                // Calcule la direction de la pouss�e
                Vector3 pushDirection = (collision.transform.position - transform.position).normalized;

                // Applique la force de pouss�e � la balle
                ballRigidbody.AddForce(pushDirection * pushForce, ForceMode.Impulse);
            }
        }
    }
}
