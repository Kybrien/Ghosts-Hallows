using UnityEngine;

public class DirectionalArrow : MonoBehaviour
{
    [SerializeField] private Transform target;

    private void Update()
    {

        if (target != null)
        {
            // On calcule la distance avec la target
            Vector3 direction = target.transform.position - transform.position;

            // On vrée une rotation basée sur la direction
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // On transformerla rotation pour que la fleche soit droite
            Quaternion finalRotation = Quaternion.Euler(targetRotation.eulerAngles.z, targetRotation.eulerAngles.y + 90, targetRotation.eulerAngles.x);

            // Appliquer la rotation
            transform.rotation = finalRotation;

        }
        else
        {
            Debug.LogWarning("Pas de Target pour l'indicateur.");
        }
    }
}