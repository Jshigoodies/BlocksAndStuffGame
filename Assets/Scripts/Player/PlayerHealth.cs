using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 200f;
    public float currentHealth;
    public float damageAmount = 5f; // Example damage value, you can adjust this as needed

    public float healAmount = 60f; // Example heal value, you can adjust this as needed

    [Header("UI Reference")]
    public Slider healthSlider; // Drag your Slider here in the Inspector

    private Collider Collider;

    void Start()
    {
        currentHealth = maxHealth;
        Collider = GetComponent<Collider>();

        if(healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
        else
        {
            Debug.LogWarning("Health Slider is not assigned in the Inspector.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnTriggerEnter(Collider other)
    {

        //Debug.Log("Player collided with: " + other.gameObject.name);


        if (other.CompareTag("Enemy"))
        {
            TakeDamage(damageAmount); // Example damage value, you can adjust this as needed


        }

        if (other.CompareTag("HealthPack"))
        {
            Heal(healAmount); // Example heal amount, you can adjust this as needed
        }


    }


    public void Heal(float healAmount)
    {
        currentHealth += healAmount;
        currentHealth = Mathf.Min(currentHealth, maxHealth); // Ensure health doesn't exceed max
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }
        //Debug.Log("Player healed " + healAmount + " health! Current health: " + currentHealth);
    }


    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }
        //Debug.Log("Player took " + damage + " damage! Current health: " + currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Player has died!");
        // Implement death logic here (e.g., respawn, game over screen, etc.)
    }
}
