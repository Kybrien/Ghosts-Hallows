using UnityEngine;

public class Bounce : MonoBehaviour
{
    public float initialBounceForce = 25f; // Force de rebond initiale
    public float minBounceForce = 5f; // Force de rebond minimale
    public float bounceForceReductionFactor = 0.2f; // Facteur de r�duction de la force de rebond
    private float currentBounceForce; // Force de rebond actuelle
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // R�cup�re le composant Rigidbody
        currentBounceForce = initialBounceForce; // Initialise la force de rebond actuelle
       
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Si la collision est avec un objet portant le tag "Joueur", ne pas r�duire la force de rebond
            currentBounceForce = initialBounceForce;
        }
        else
        {
            // Si la collision est avec un autre objet, r�duit la force de rebond
            currentBounceForce = Mathf.Max(minBounceForce, currentBounceForce * bounceForceReductionFactor);
        }

        // Inverse la direction de la v�locit� en Y et applique la force de rebond
        rb.velocity = new Vector3(rb.velocity.x, currentBounceForce, rb.velocity.z);
    }
}

