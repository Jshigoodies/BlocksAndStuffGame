using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public int health = 100;
    public float knockbackForce = 5f;

    public GameObject explosionPrefab; // Assign an explosion prefab in the Inspector

    private Collider enemyCollider;
    private Rigidbody enemyRigidbody;
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    [Header("Audio Settings")]
    public AudioSource enemyAudioSource; // Assign an AudioSource component in the Inspector
    public AudioClip hitSound; // Assign a hit sound clip in the Inspector

    void Start()
    {
        enemyCollider = GetComponent<Collider>();
        enemyRigidbody = GetComponent<Rigidbody>();

        
        enemyAudioSource = GetComponent<AudioSource>();


        enemyCollider.isTrigger = true; // Enemy won't collide with other objects until hit
        enemyRigidbody.isKinematic = true; // Enemy won't be affected by physics until hit


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        // Implement health reduction logic here
        // If health <= 0, call Die()
        //Debug.Log(gameObject.name + " took " + damage + " damage!");

        health = health - damage;

        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, transform.rotation);
            enemyAudioSource.PlayOneShot(hitSound);
        }

        if (health <= 0)
        {
            Die();
        }

    }

    public void Die()
    {
        enemyCollider.isTrigger = false; // Enemy can now collide with other objects
        enemyRigidbody.isKinematic = false; // Enemy can now be affected by physics (e.g., knocked back)

        Vector3 randomDirection = Random.onUnitSphere;
        enemyRigidbody.AddForce(randomDirection * knockbackForce, ForceMode.Impulse); // Example knockback force

        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, transform.rotation);
        }

        //Debug.Log(gameObject.name + " died!");

        Destroy(gameObject, 3f);
    }
}
