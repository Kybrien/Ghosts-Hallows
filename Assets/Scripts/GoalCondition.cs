using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GoalCondition : MonoBehaviour
{
    public Transform respawnPoint; // Point de respawn de la balle
    public int scoreValue = 1; // Valeur du score à ajouter
    public int scoreJ1 = 0; // Score actuel
    public int scoreJ2 = 0; // Score actuel
    private Rigidbody rb;
    public TMPro.TMP_Text J1Score;
    public TMPro.TMP_Text J2Score;
    public TMPro.TMP_Text Winner;

    void Start()
    {
        // Récupère le Rigidbody attaché à la balle
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        CheckWinner();
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
                J1Score.text = "" + scoreJ1;
            }
            else if (gameObject.CompareTag("GoalP2"))
            {
                scoreJ2 += scoreValue;
                Debug.Log("But marqué ! Score J2 : " + scoreJ2);
                J2Score.text = "" + scoreJ2;
            }


            // Réinitialise la position de la balle au point de respawn
            collision.gameObject.transform.position = respawnPoint.position;
        }
    }
    void CheckWinner()
    {
        if (scoreJ1 >= 5)
        {
            Debug.Log("Player 1 Wins!");
            Winner.text = "" + "Player 1 Wins!";
            ResetGame();
        }
        else if (scoreJ2 >= 5)
        {
            Debug.Log("Player 2 Wins!");
            Winner.text = "" + "Player 2 Wins!";
            ResetGame();
        }
    }

    void ResetGame()
    {
        // Reset scores, ball position, and any other necessary game state
        scoreJ1 = 0;
        scoreJ2 = 0;
        J1Score.text = "0";
        J2Score.text = "0";
        Invoke("ResetScene", 2);

    }
    void ResetScene()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);

    }


}
