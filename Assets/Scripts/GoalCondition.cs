using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalCondition : MonoBehaviour
{
    public Transform respawnPoint; // Point de respawn de la balle
    public int scoreValue = 1; // Valeur du score à ajouter
    public int scoreJ1 = 0; // Score actuel
    public int scoreJ2 = 0; // Score actuel
    private Rigidbody rb;
    public TMPro.TMP_Text playerOverlayScore;

    void Start()
    {
        // Récupère le Rigidbody attaché à la balle
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Ball"))
        {
            Debug.Log("Test");
            if (gameObject.CompareTag("GoalP1"))
            {
                scoreJ1 += scoreValue;
                Debug.Log("But marqué ! Score J1 : " + scoreJ1);
                UpdateScoreDisplay();
            }
            else if (gameObject.CompareTag("GoalP2"))
            {
                scoreJ2 += scoreValue;
                Debug.Log("But marqué ! Score J2 : " + scoreJ2);
                UpdateScoreDisplay();
            }


            // Réinitialise la position de la balle au point de respawn
            collision.gameObject.transform.position = respawnPoint.position;
        }
    }
    void UpdateScoreDisplay()
    {
        playerOverlayScore.text = scoreJ1 + " - " + scoreJ2 ;
    }
}
