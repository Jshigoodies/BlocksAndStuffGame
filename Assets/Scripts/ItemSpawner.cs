using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject healthPrefab;
    public float spawnInterval = 0.2f; // Fast spawning!
    public Vector2 areaSize = new Vector2(70f, 71f);

    void Start()
    {
        // Starts immediately (0f) and repeats every 0.2 seconds
        InvokeRepeating(nameof(SpawnHealth), 0f, spawnInterval);
    }

    void SpawnHealth()
    {
        // Randomize coordinates within the 20x20 box
        float randomX = Random.Range(-areaSize.x / 2, areaSize.x / 2);
        float randomZ = Random.Range(-areaSize.y / 2, areaSize.y / 2);

        // Calculate final position relative to the spawner's location
        Vector3 spawnPosition = new Vector3(randomX, 0.5f, randomZ) + transform.position;

        // Spawn the prefab
        Instantiate(healthPrefab, spawnPosition, Quaternion.identity);
    }

    // Visualizes the 20x20 area in the Scene view
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(areaSize.x, 1, areaSize.y));
    }
}