using UnityEngine;

public class ChangeMeshOnCollision : MonoBehaviour
{
    public GameObject playerMeshObject; // Assignez ici le GameObject du joueur ayant le SkinnedMeshRenderer

    private SkinnedMeshRenderer playerMeshRenderer;
    [SerializeField] private GameObject FX_skinChanging;

    void Start()
    {
        if (playerMeshObject == null)
        {
            Debug.LogError("Mesh du player non assigné");
            return;
        }

        // On initialise le composant SkinnedMeshRenderer du joueur
        playerMeshRenderer = playerMeshObject.GetComponent<SkinnedMeshRenderer>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ghost"))
        {
            // On cherchezle SkinnedMeshRenderer dans le sous-objet du fantôme
            SkinnedMeshRenderer ghostMeshRenderer = collision.gameObject.transform.Find("GhostMesh").GetComponent<SkinnedMeshRenderer>();

            if (ghostMeshRenderer != null && playerMeshRenderer != null)
            {
                Debug.Log("Conditions remplises");
                // Change le mesh et le matériel du joueur pour celui du fantôme
                playerMeshRenderer.material = ghostMeshRenderer.material;
                playerMeshRenderer.sharedMesh = ghostMeshRenderer.sharedMesh;

                //On fait apparaitre le fx voulu
                Instantiate(FX_skinChanging, transform.position, Quaternion.identity);


            }
        }
    }
}
