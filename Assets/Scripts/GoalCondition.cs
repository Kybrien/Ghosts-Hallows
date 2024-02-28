using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalCondition : MonoBehaviour
{
    public Transform respawnPoint; // Point de respawn de la balle
    public int scoreValue = 1; // Valeur du score à ajouter
    private int score = 0; // Score actuel
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
            // Incrémente le score
            score += scoreValue;
            Debug.Log("But marqué ! Score : " + score);

            // Réinitialise la position de la balle au point de respawn
            collision.gameObject.transform.position = respawnPoint.position;
        }
    }
}
