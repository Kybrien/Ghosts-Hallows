using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterGoalTP : MonoBehaviour
{
    public Transform respawnPointPlayer1; // Position de t�l�portation pour le premier joueur
    public Transform respawnPointPlayer2; // Position de t�l�portation pour le deuxi�me joueur
    public Transform targetDirectionPlayer1; // Direction cible pour le premier joueur
    public Transform targetDirectionPlayer2; // Direction cible pour le deuxi�me joueur

    [SerializeField] private GameObject FX_OnGoal;


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Instantiate(FX_OnGoal, transform.position, Quaternion.identity);
            TeleportPlayers();
        }
    }

    // M�thode pour t�l�porter les joueurs
    public void TeleportPlayers()
    {
        // T�l�porte le premier joueur � la position de respawn et orienter dans la direction cible
        GameObject player1 = GameObject.Find("Player");
        if (player1 != null)
        {
            player1.transform.position = respawnPointPlayer1.position;
            player1.transform.LookAt(targetDirectionPlayer1);
        }

        // T�l�porte le deuxi�me joueur � la position de respawn et orienter dans la direction cible
        GameObject player2 = GameObject.Find("Player(Clone)");
        if (player2 != null)
        {
            player2.transform.position = respawnPointPlayer2.position;
            player2.transform.LookAt(targetDirectionPlayer2);
        }
    }
}