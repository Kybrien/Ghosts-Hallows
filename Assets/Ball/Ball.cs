using UnityEngine;

public class SoccerBall : MonoBehaviour
{
    public float kickForce = 7f; // Force du coup de pied

    private Rigidbody rb;

    void Start()
    {
        // Récupère le Rigidbody attaché à la balle
        rb = GetComponent<Rigidbody>();

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            // Calcule la direction du coup de pied
            Vector3 kickDirection = (collision.transform.position - transform.position).normalized;

            // Applique la force du coup de pied à la balle
            rb.AddForce(kickDirection * kickForce, ForceMode.Impulse);
        }

        if (collision.gameObject.CompareTag("GoalP1") || collision.gameObject.CompareTag("GoalP2"))
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

        }
    }
}