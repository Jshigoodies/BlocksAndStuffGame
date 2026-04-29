using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathBarrier : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Reload the current scene when the player enters the death barrier
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}

