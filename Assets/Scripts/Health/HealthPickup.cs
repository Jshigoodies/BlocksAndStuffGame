using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    private AudioSource pickupSoundSource; // Assign the pickup sound in the Inspector
    public AudioClip healthSound; // Assign the pickup sound in the Inspector
    void Start()
    {
        pickupSoundSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Play the pickup sound
            if (pickupSoundSource != null)
            {
                pickupSoundSource.PlayOneShot(healthSound);
                GetComponent<Collider>().enabled = false;
                GetComponent<MeshRenderer>().enabled = false;
                Destroy(gameObject, pickupSoundSource.clip.length);
            }
            else
            {
                Debug.LogWarning("Pickup sound is not assigned in the Inspector.");
            }
            // Destroy the health pickup after a short delay to allow the sound to play
            
        }
    }

}
