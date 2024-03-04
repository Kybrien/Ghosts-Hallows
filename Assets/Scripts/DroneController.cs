using UnityEngine;

public class DroneController : MonoBehaviour
{
    public GameObject ball; // Référence à la balle
    public float moveSpeed = 5f; // Vitesse de déplacement du drone
    public float grabRange = 2f; // Portée d'attrapage de la balle
    public float launchForce = 20f; // Force de lancement de la balle après l'avoir attrapée
    public float grabDuration = 3f; // Durée d'attrapage de la balle
    private bool isGrabbing = false; // Indique si le drone attrape actuellement la balle
    private float grabTimer = 0f; // Compteur de temps pour l'attrapage de la balle
    private Rigidbody ballRigidbody; // Référence au Rigidbody de la balle

    void Start()
    {
        ballRigidbody = ball.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!isGrabbing)
        {
            // Calculer la direction vers la balle
            Vector3 directionToBall = ball.transform.position - transform.position;
            directionToBall.y = 0; // Ne pas prendre en compte la composante Y

            // Déplacer le drone vers la balle
            transform.Translate(directionToBall.normalized * moveSpeed * Time.deltaTime, Space.World);

            // Vérifier si le drone est assez proche de la balle pour l'attraper
            if (directionToBall.magnitude <= grabRange)
            {
                // Commencer à attraper la balle
                isGrabbing = true;
                grabTimer = 0f;

            }
        }
        else
        {
            // Incrémenter le compteur de temps pour l'attrapage de la balle
            grabTimer += Time.deltaTime;

            // Si le temps d'attrapage est écoulé, lancer la balle dans une direction aléatoire
            if (grabTimer >= grabDuration)
            {
                LaunchBall();
            }
        }
    }
    void LaunchBall()
    {
        // Calculer une direction aléatoire pour lancer la balle
        Vector3 launchDirection = Random.insideUnitSphere.normalized;

        // Appliquer une force à la balle dans la direction aléatoire
        ballRigidbody.AddForce(launchDirection * launchForce, ForceMode.Impulse);

        // Réinitialiser les paramètres d'attrapage de la balle
        isGrabbing = false;

        // Réactiver le Rigidbody de la balle
        ballRigidbody.isKinematic = false;
    }
}