using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 200f;
    public float currentHealth;
    public float damageAmount = 5f; // Example damage value, you can adjust this as needed

    public float healAmount = 60f; // Example heal value, you can adjust this as needed

    [Header("UI Reference")]
    public Slider healthSlider; // Drag your Slider here in the Inspector

    private Collider Collider;
    private bool isBlocking = false;
    private Animator animator;


    //all this to handle the goddam sheild
    public float maxHoldTime = 3f;
    public float cooldownDuration = 2f;
    public Slider shieldBar;

    private float holdTimer = 0f;
    private bool isOnCooldown = false;

    void Start()
    {
        currentHealth = maxHealth;
        Collider = GetComponent<Collider>();
        animator = GetComponent<Animator>();

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
        else
        {
            Debug.LogWarning("Health Slider is not assigned in the Inspector.");
        }
        if (shieldBar != null)
        {
            shieldBar.maxValue = maxHoldTime;
            shieldBar.value = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 1. Handle the Overheat/Cooldown state
        if (isOnCooldown)
        {
            // During cooldown, the "heat" (holdTimer) drains back to zero
            holdTimer -= Time.deltaTime * 1.5f; // Recharges 1.5x faster than it drains

            if (holdTimer <= 0)
            {
                holdTimer = 0;
                isOnCooldown = false;
                Debug.Log("Shield Ready!");
            }

            // Force blocking to false while overheated
            isBlocking = false;
        }
        else
        {
            bool wantsToBlock = Mouse.current.rightButton.isPressed;

            if (wantsToBlock)
            {
                isBlocking = true;
                holdTimer += Time.deltaTime; // Add "heat"

                if (holdTimer >= maxHoldTime)
                {
                    StartCooldown();
                }
            }
            else
            {
                isBlocking = false;
                // ANTI-SPAM: Instead of holdTimer = 0, we drain it slowly
                if (holdTimer > 0)
                {
                    holdTimer -= Time.deltaTime * 0.75f; // Slowly recover shield energy
                }
            }
        }

        // UPDATE THE UI
        if (shieldBar != null)
        {
            // We show the "remaining" energy by doing Max - Current Heat
            shieldBar.value = maxHoldTime - holdTimer;
        }

        // Update Animator
        animator.SetBool("isBlocking", isBlocking);
    }

    void StartCooldown()
    {
        isBlocking = false;
        isOnCooldown = true;
        // We don't reset holdTimer to 0 here anymore; 
        // the Update loop will drain it during the cooldown period.
        //Debug.Log("Shield overheated! Wait for it to cool down.");
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
        if(isBlocking)
        {
            return; // No damage taken if blocking
        }

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
