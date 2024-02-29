using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalCondition : MonoBehaviour
{
    public Transform respawnPoint; // Point de respawn de la balle
    public int scoreValue = 1; // Valeur du score � ajouter
    private int score = 0; // Score actuel
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
            // Incr�mente le score
            score += scoreValue;
            Debug.Log("But marqu� ! Score : " + score);

            // R�initialise la position de la balle au point de respawn
            collision.gameObject.transform.position = respawnPoint.position;
        }
    }
}
