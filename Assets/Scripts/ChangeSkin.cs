using UnityEngine;

public class ChangeMeshOnCollision : MonoBehaviour
{
    public GameObject playerMeshObject; // Assignez ici le GameObject du joueur ayant le SkinnedMeshRenderer

    private SkinnedMeshRenderer playerMeshRenderer;

    void Start()
    {
        if (playerMeshObject == null)
        {
            Debug.LogError("Player mesh object is not assigned!");
            return;
        }

        // Initialiser le composant SkinnedMeshRenderer du joueur
        playerMeshRenderer = playerMeshObject.GetComponent<SkinnedMeshRenderer>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ghost"))
        {
            // Cherchez le SkinnedMeshRenderer dans le sous-objet du fantôme
            SkinnedMeshRenderer ghostMeshRenderer = collision.gameObject.transform.Find("GhostMesh").GetComponent<SkinnedMeshRenderer>();

            if (ghostMeshRenderer != null && playerMeshRenderer != null)
            {
                Debug.Log("Conditions remplises");
                // Change le mesh et le matériel du joueur pour celui du fantôme
                playerMeshRenderer.material = ghostMeshRenderer.material;
                playerMeshRenderer.sharedMesh = ghostMeshRenderer.sharedMesh;
            }
        }
    }
}
