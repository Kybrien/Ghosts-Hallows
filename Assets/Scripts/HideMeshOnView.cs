using UnityEngine;

public class HideMeshOnView : MonoBehaviour
{
    public Transform cameraTransform;
    public Renderer playerRenderer;

    void Update()
    {
        if (cameraTransform != null && playerRenderer != null)
        {
            // D�tecte si la cam�ra est orient�e vers le bas � un certain angle.
            // Vous pouvez ajuster l'angle selon vos besoins.
            if (Vector3.Angle(cameraTransform.forward, Vector3.down) < 70)
            {
                playerRenderer.enabled = false; // Cache le mesh si la cam�ra est orient�e vers le bas.
            }
            else
            {
                playerRenderer.enabled = true; // Affiche le mesh autrement.
            }
        }
    }
}
