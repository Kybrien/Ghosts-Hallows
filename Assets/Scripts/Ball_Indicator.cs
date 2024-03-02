using UnityEngine;

public class DirectionalArrow : MonoBehaviour
{
    [SerializeField] private Transform target;

    private void Update()
    {

        if (target != null)
        {
            // Calculer la direction vers la cible
            Vector3 direction = target.transform.position - transform.position;

            // Créer une rotation basée sur la direction
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Transformer la rotation pour s'adapter à votre orientation spécifique
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