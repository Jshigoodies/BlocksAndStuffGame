using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("Targeting")]
    public Transform player;
    public float attackDistance = 2.0f;

    [Header("Combat")]
    public float attackCooldown = 1.5f;
    private float nextAttackTime = 0f;

    private NavMeshAgent agent;
    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        if(player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if(playerObj != null)
            {
                player = playerObj.transform;
            }
            else
            {
                Debug.LogError("Player not found in the scene. Please assign the player Transform or tag the player with 'Player'.");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;

        // Calculate the distance between the enemy and the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackDistance)
        {
            // We are close enough. Stop moving and face the player.
            agent.isStopped = true;
            FacePlayer();

            // Check if enough time has passed to attack again
            if (Time.time >= nextAttackTime)
            {
                AttackPlayer();
                nextAttackTime = Time.time + attackCooldown;
            }
        }
        else
        {
            // Player is out of range. Chase them!
            agent.isStopped = false;
            agent.SetDestination(player.position);

            // Optional: If you have a walking animation, handle it here
            // animator.SetBool("IsWalking", true);
        }
    }

    void AttackPlayer()
    {
        // Fire the trigger in the Animator to play the attack animation
        animator.SetTrigger("Attack");

    }

    void FacePlayer()
    {
        // Calculate the direction to the player
        Vector3 direction = (player.position - transform.position).normalized;
        // Ensure the enemy doesn't tilt up/down to look at the player
        direction.y = 0;

        // Rotate smoothly towards the player
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}

