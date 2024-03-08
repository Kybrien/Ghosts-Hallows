using UnityEngine;
using System.Collections;


public class DroneController : MonoBehaviour
{
    public GameObject[] cubes; // R�f�rences aux cubes sur la carte
    public GameObject ball; // R�f�rence � la balle
    public GameObject projectilePrefab; // Prefab du projectile
    public float moveSpeed = 5f; // Vitesse de d�placement du drone en mode Roaming
    public float chasingMoveSpeed = 10f; // Vitesse de d�placement du drone en mode Chasing
    public float grabRange = 2f; // Port�e d'attrapage de la balle
    public float roamingDuration = 10f; // Dur�e de la phase Roaming en secondes
    public float chasingDuration = 5f; // Dur�e de la phase Chasing en secondes

    private Rigidbody ballRigidbody; // R�f�rence au Rigidbody de la balle
    private bool isChasing = false; // Indique si le drone est en train de chasser la balle
    private GameObject targetCube; // Cube cible pour la phase de d�placement entre les cubes
    private float timer = 0f; // Compteur de temps pour la phase actuelle
    private bool ballIsGrabbed = false; // Indique si la balle est attrap�e par le drone
    private Collider ballCollider; // R�f�rence au Collider de la balle
    private float lastProjectileTime = 0f; // Temps du dernier tir de projectile
    public float projectileSpeed = 10f; // Vitesse du projectile
    public float projectileCooldown = 15f; // Temps de recharge entre chaque tir de projectile


    void Start()
    {
        ballCollider = ball.GetComponent<Collider>();
        ballRigidbody = ball.GetComponent<Rigidbody>();
        SetNextTargetCube(); // D�finir le premier cube cible
    }

    void Update()
    {
        timer += Time.deltaTime;
        // Mettre � jour le timer depuis le dernier tir de projectile
        lastProjectileTime += Time.deltaTime;

        if (!isChasing)
        {
            // Phase Roaming
            if (timer >= roamingDuration)
            {
                Debug.Log("Changement de phase : Chasing");
                isChasing = true;
                timer = 0f; // R�initialiser le compteur de temps
                ballIsGrabbed = false; // R�initialiser l'�tat de la balle
                ballCollider.enabled = true; // Activer le collider de la balle
                ballRigidbody.useGravity = true; // Activer la gravit� de la balle
            }
            else
            {
                if (lastProjectileTime >= projectileCooldown)
                {
                    // Tirer le projectile
                    FireProjectile();

                    // R�initialiser le temps du dernier tir de projectile
                    lastProjectileTime = 0f;
                }
                // Si le drone n'a pas la balle, se d�placer entre les cubes
                MoveBetweenCubes();
            }
        }


        else
        {
            // Phase Chasing
            if (timer >= chasingDuration)
            {
                Debug.Log("Changement de phase : Roaming");
                isChasing = false;
                timer = 0f; // R�initialiser le compteur de temps
                ballIsGrabbed = false; // R�initialiser l'�tat de la balle
                ballCollider.enabled = true; // Activer le collider de la balle
                ballRigidbody.useGravity = true; // Activer la gravit� de la balle
            }
            else
            {
                MoveDroneToTarget();
            }
        }
    }
    void FireProjectile()
    {
        // Calculer la direction du tir vers la balle
        Vector3 fireDirection = (ball.transform.position - transform.position).normalized;

        // Instancier le projectile � partir du prefab � la position du drone
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        // R�cup�rer le Rigidbody du projectile
        Rigidbody projectileRigidbody = projectile.GetComponent<Rigidbody>();

        // Appliquer une force au projectile dans la direction du tir
        projectileRigidbody.AddForce(fireDirection * projectileSpeed, ForceMode.Impulse);

        // D�truire le projectile apr�s 10 secondes
        Destroy(projectile, 10f);
    }




    void MoveBetweenCubes()
    {
        // Si le drone est assez proche du cube cible, choisir le prochain cube cible
        if (Vector3.Distance(transform.position, targetCube.transform.position) < 0.1f)
        {
            SetNextTargetCube();
        }
        else
        {
            // D�placer le drone vers le cube cible
            transform.position = Vector3.MoveTowards(transform.position, targetCube.transform.position, moveSpeed * Time.deltaTime);
        }
    }

    void SetNextTargetCube()
    {
        // Choisir al�atoirement l'un des cubes comme prochaine cible
        targetCube = cubes[Random.Range(0, cubes.Length)];
    }

    void MoveDroneToTarget()
    {
        // Si le drone est suffisamment proche de la balle, la saisir
        if (Vector3.Distance(transform.position, ball.transform.position) <= grabRange)
        {
            GrabBall();
        }
        else
        {
            // D�placer le drone vers la balle avec la vitesse appropri�e
            float speed = isChasing ? chasingMoveSpeed : moveSpeed;
            transform.position = Vector3.MoveTowards(transform.position, ball.transform.position, speed * Time.deltaTime);
        }
    }

    void GrabBall()
    {
        // D�sactiver le collider de la balle pour �viter les interactions pendant qu'elle est tenue par le drone
        ballCollider.enabled = false;
        ballRigidbody.velocity = Vector3.zero;
        ballRigidbody.angularVelocity = Vector3.zero;
        // D�sactiver la gravit� de la balle pendant qu'elle est tenue par le drone
        ballRigidbody.useGravity = false;

        // Fixer la position de la balle � celle du drone
        ball.transform.position = transform.position;



        // Mettre � jour l'�tat de la balle attrap�e
        ballIsGrabbed = true;

        if (ballIsGrabbed == true)
        {
            MoveBetweenCubes();
        }
    }
}
