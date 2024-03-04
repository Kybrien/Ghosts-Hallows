using UnityEngine;

public class DroneController : MonoBehaviour
{
    public GameObject ball; // R�f�rence � la balle
    public float moveSpeed = 5f; // Vitesse de d�placement du drone
    public float grabRange = 2f; // Port�e d'attrapage de la balle
    public float launchForce = 20f; // Force de lancement de la balle apr�s l'avoir attrap�e
    public float grabDuration = 3f; // Dur�e d'attrapage de la balle
    private bool isGrabbing = false; // Indique si le drone attrape actuellement la balle
    private float grabTimer = 0f; // Compteur de temps pour l'attrapage de la balle
    private Rigidbody ballRigidbody; // R�f�rence au Rigidbody de la balle

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

            // D�placer le drone vers la balle
            transform.Translate(directionToBall.normalized * moveSpeed * Time.deltaTime, Space.World);

            // V�rifier si le drone est assez proche de la balle pour l'attraper
            if (directionToBall.magnitude <= grabRange)
            {
                // Commencer � attraper la balle
                isGrabbing = true;
                grabTimer = 0f;

            }
        }
        else
        {
            // Incr�menter le compteur de temps pour l'attrapage de la balle
            grabTimer += Time.deltaTime;

            // Si le temps d'attrapage est �coul�, lancer la balle dans une direction al�atoire
            if (grabTimer >= grabDuration)
            {
                LaunchBall();
            }
        }
    }
    void LaunchBall()
    {
        // Calculer une direction al�atoire pour lancer la balle
        Vector3 launchDirection = Random.insideUnitSphere.normalized;

        // Appliquer une force � la balle dans la direction al�atoire
        ballRigidbody.AddForce(launchDirection * launchForce, ForceMode.Impulse);

        // R�initialiser les param�tres d'attrapage de la balle
        isGrabbing = false;

        // R�activer le Rigidbody de la balle
        ballRigidbody.isKinematic = false;
    }
}