using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;

public class MobileTurretAI : MonoBehaviour
{
    [Header("Targeting")]
    public Transform player;
    public Transform turretHead; // Drag your Turret_Head here
    public Transform firePoint;  // Drag your FirePoint here

    [Header("Ranges")]
    public float aggroRange = 15f;  // When the turret starts chasing
    public float attackRange = 8f;  // When the turret stops moving to shoot

    [Header("Combat Stats")]
    public float turnSpeed = 5f;
    public float fireRate = 1f;     // Shots per second
    public GameObject projectilePrefab;

    private NavMeshAgent agent;
    private float fireCountdown = 0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {

        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
            else
            {
                Debug.LogError("Player not found in the scene. Please assign the player Transform or tag the player with 'Player'.");
            }
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // 1. Chasing Logic
        if (distanceToPlayer <= aggroRange)
        {
            if (distanceToPlayer > attackRange)
            {
                // Chase the player
                agent.isStopped = false;
                agent.SetDestination(player.position);
            }
            else
            {
                // In range: Stop moving and attack
                agent.isStopped = true;
                AimAndFire();
            }
        }
        else
        {
            // Player is out of aggro range
            agent.isStopped = true;
        }
    }

    void AimAndFire()
    {
        // 2. Aiming Logic (Rotate only the head)
        Vector3 direction = (player.position - turretHead.position).normalized;

        // Optional: Keep the turret head perfectly level by ignoring Y-axis height differences
        direction.y = 0;

        Quaternion lookRotation = Quaternion.LookRotation(direction);
        turretHead.rotation = Quaternion.Slerp(turretHead.rotation, lookRotation, Time.deltaTime * turnSpeed);

        // 3. Firing Logic
        fireCountdown -= Time.deltaTime;
        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate; // Reset timer
        }
    }

    void Shoot()
    {
        // Spawn the projectile at the FirePoint's position and rotation
        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // You can add muzzle flash particles or sound effects here!
    }
}