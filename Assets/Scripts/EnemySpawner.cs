using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Array of enemy prefabs to spawn
    public float spawnInterval = 2f; // Time interval between spawns
    public Vector2 areaSize = new Vector2(20f, 20f); // Size of the area where enemies will spawn

    void Start()
    {
        InvokeRepeating(nameof(SpawnSingleEnemy), 0f, spawnInterval); // Start spawning enemies at regular intervals
    }

    void SpawnSingleEnemy()
    {
        if (enemyPrefabs.Length == 0)
        {
            Debug.LogWarning("No enemy prefabs assigned to the EnemySpawner.");
            return;
        }
        // Randomly select an enemy prefab from the array
        GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        // Generate a random position within the defined area
        Vector3 spawnPosition = new Vector3(
            Random.Range(-areaSize.x / 2, areaSize.x / 2),
            0f, 
            Random.Range(-areaSize.y / 2, areaSize.y / 2)
        );
        // Instantiate the enemy at the generated position
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
