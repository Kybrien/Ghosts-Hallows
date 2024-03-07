using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class TP : MonoBehaviour
{
    public Transform respawnPoint;
    public Transform targetDirection;

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Contact");
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Contact Player");
            // Appel d'une méthode pour déplacer le joueur
            TeleportPlayer(collision.gameObject);
        }
    }

    // Méthode pour déplacer le joueur
    void TeleportPlayer(GameObject player)
    {
        player.transform.position = respawnPoint.position;
        player.transform.LookAt(targetDirection);
        Debug.Log("TP");
    }
}