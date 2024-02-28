using UnityEngine;

public class TP : MonoBehaviour
{
    public Transform respawnPoint;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Appel d'une m�thode pour d�placer le joueur
            TeleportPlayer(collision.gameObject);
        }
    }

    // M�thode pour d�placer le joueur
    void TeleportPlayer(GameObject player)
    {
        player.transform.position = respawnPoint.position;
        Debug.Log("TP");
    }
}
