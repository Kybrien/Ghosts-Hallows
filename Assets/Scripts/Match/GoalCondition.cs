using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalCondition : MonoBehaviour
{
    public Transform respawnPoint; // Point de respawn de la balle
    public int scoreValue = 1; // Valeur du score à ajouter
    private int scoreJ1 = 0; // Score actuel
    private int scoreJ2 = 0; // Score actuel
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
            if (gameObject.CompareTag("GoalP1"))
            {
                scoreJ1 += scoreValue;
                Debug.Log("But marqué ! Score J1 : " + scoreJ1);
                J1Score.text = "" + scoreJ1;

                Winner.text = "GOAAAAAAAL ! Player1";
                Invoke("CleanMessage", 2);

            }
            if (gameObject.CompareTag("GoalP2"))
            {
                scoreJ2 += scoreValue;
                Debug.Log("But marqué ! Score J2 : " + scoreJ2);
                J2Score.text = "" + scoreJ2;
                Winner.text = "GOAAAAAAAL ! Player1";
                Invoke("CleanMessage", 2);
            }


            // Réinitialise la position de la balle au point de respawn
            collision.gameObject.transform.position = respawnPoint.position;
            collision.gameObject.SetActive(true);
        }
    }
    void CleanMessage()
    {
        Winner.text = "";
    }
    void CheckWinner()
    {
        if (scoreJ1 >= 3)
        {
            Debug.Log("Player 1 Wins!");
            Winner.text = "" + "Player 1 Wins!";
            ResetGame();
        }
        else if (scoreJ2 >= 3)
        {
            Debug.Log("Player 2 Wins!");
            Winner.text = "" + "Player 2 Wins!";
            ResetGame();
        }
    }
    public void CheckWinnerOnTimer()
    {
        if (scoreJ1 > scoreJ2)
        {
            /*Debug.Log("Player 1 Wins!");*/
            Winner.text = "" + "Player 1 Wins!";
            ResetGame();
        }
        if (scoreJ1 < scoreJ2)
        {
            /*Debug.Log("Player 2 Wins!");*/
            Winner.text = "" + "Player 2 Wins!";
            ResetGame();
        }
        else
        {
            /*Debug.Log("Egalite");*/
            Winner.text = "" + "Egalité !";
            ResetGame();
        }
    }

    void ResetGame()
    {

        Invoke("ResetScene", 2);

    }
    void ResetScene()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);

    }


}
