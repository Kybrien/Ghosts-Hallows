using UnityEngine;
using System.Collections.Generic;

public class GameTransition : MonoBehaviour
{
    // Dictionnaire pour stocker les points de respawn en fonction des playerID
    public Dictionary<int, Transform> playerRespawnPoints = new Dictionary<int, Transform>();
    public Transform targetDirection;

    void OnCollisionEnter(Collision collision)
    {
        // Vérifie si le GameObject qui a déclenché la collision a un PlayerID
        PlayerID playerIDComponent = collision.gameObject.GetComponent<PlayerID>();
        if (playerIDComponent != null && playerRespawnPoints.ContainsKey(playerIDComponent.ID))
        {
            // Téléporte le joueur au point de respawn correspondant à son ID
            TeleportPlayer(collision.gameObject, playerRespawnPoints[playerIDComponent.ID]);
        }
    }

    // Méthode pour déplacer le joueur au point de respawn spécifique
    void TeleportPlayer(GameObject player, Transform respawnPoint)
    {
        player.transform.position = respawnPoint.position;
        player.transform.LookAt(targetDirection);
        Debug.Log("TP");
    }
}
