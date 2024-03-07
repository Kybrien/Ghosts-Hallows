using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSkin : MonoBehaviour
{
    private MeshRenderer playerMeshRenderer;
    private MeshFilter playerMeshFilter;

    void Start()
    {
        // Initialiser les composants MeshRenderer et MeshFilter du joueur
        playerMeshRenderer = GetComponent<MeshRenderer>();
        playerMeshFilter = GetComponent<MeshFilter>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ghost"))
        {
            MeshRenderer ghostMeshRenderer = collision.gameObject.GetComponent<MeshRenderer>();
            MeshFilter ghostMeshFilter = collision.gameObject.GetComponent<MeshFilter>();

            if (ghostMeshRenderer != null && ghostMeshFilter != null)
            {
                // Change le mesh et le matériel du joueur pour celui du fantôme
                playerMeshRenderer.material = ghostMeshRenderer.material;
                playerMeshFilter.mesh = ghostMeshFilter.mesh;
            }
        }
    }
}
