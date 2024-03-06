using UnityEngine;
using System.Collections.Generic;

public class GameTransition : MonoBehaviour
{
    // Dictionnaire pour stocker les points de respawn en fonction des playerID
    public Dictionary<int, Transform> playerRespawnPoints = new Dictionary<int, Transform>();
    public Transform targetDirection;

    void OnCollisionEnter(Collision collision)
    {
        // V�rifie si le GameObject qui a d�clench� la collision a un PlayerID
        PlayerID playerIDComponent = collision.gameObject.GetComponent<PlayerID>();
        if (playerIDComponent != null && playerRespawnPoints.ContainsKey(playerIDComponent.ID))
        {
            // T�l�porte le joueur au point de respawn correspondant � son ID
            TeleportPlayer(collision.gameObject, playerRespawnPoints[playerIDComponent.ID]);
        }
    }

    // M�thode pour d�placer le joueur au point de respawn sp�cifique
    void TeleportPlayer(GameObject player, Transform respawnPoint)
    {
        player.transform.position = respawnPoint.position;
        player.transform.LookAt(targetDirection);
        Debug.Log("TP");
    }
}
