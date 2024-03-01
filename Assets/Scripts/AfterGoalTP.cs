using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterGoalTP : MonoBehaviour
{
    public Transform respawnPointPlayer1; // Position de t�l�portation pour le premier joueur
    public Transform respawnPointPlayer2; // Position de t�l�portation pour le deuxi�me joueur

    void OnCollisionEnter(Collision collision)
    {
        GameObject player1 = GameObject.Find("Player");
        GameObject player2 = GameObject.Find("Player(Clone)");

        if (collision.gameObject.CompareTag("Ball"))
        {
            TeleportPlayer();
        }
    }

    // M�thode pour t�l�porter un joueur en fonction de son ID
    void TeleportPlayer()
    {
        // Recherche le joueur dans la sc�ne en utilisant le nom correct
        GameObject player1 = GameObject.Find("Player");
        GameObject player2 = GameObject.Find("Player(Clone)");
        player1.transform.position = respawnPointPlayer1.position;
        player2.transform.position = respawnPointPlayer2.position;

    }
}