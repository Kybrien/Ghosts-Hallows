using UnityEngine;

public class TP : MonoBehaviour
{
    public Transform respawnPoint;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Appel d'une méthode pour déplacer le joueur
            TeleportPlayer(collision.gameObject);
        }
    }

    // Méthode pour déplacer le joueur
    void TeleportPlayer(GameObject player)
    {
        player.transform.position = respawnPoint.position;
        Debug.Log("TP");
    }
}
