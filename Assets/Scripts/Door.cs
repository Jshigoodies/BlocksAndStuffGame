using UnityEngine;
using UnityEngine.SceneManagement;
public class Door : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("LevelTwo");
        }
            
    }
}
