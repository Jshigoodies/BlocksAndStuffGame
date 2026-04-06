using UnityEditor.UI;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    private Animator anim;
    private Collider swordCollider;

    public float attackCooldown = 0.5f;
    private float nextAttackTime = 0f;
    public int damage = 20;


    void Start()
    {
        anim = GetComponent<Animator>();
        swordCollider = GetComponent<Collider>();

        swordCollider.enabled = false; // Disable collider at start
        swordCollider.isTrigger = true; // Set collider to trigger so it doesn't cause physical collisions
    }

    
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    void Attack()
    {
        anim.SetTrigger("Attack");
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("sword entered");
        if (other.CompareTag("Enemy"))
        {
            // If the enemy has a script with a TakeDamage function:
            // other.GetComponent<EnemyHealth>()?.TakeDamage(damage);

            //Debug.Log("Slashed " + other.name);
            other.GetComponent<EnemyManager>()?.TakeDamage(damage);
        }
    }

    public void StartAttack()
    {
        swordCollider.enabled = true;
        //Debug.Log("sword collider enabled");
    }
    public void EndAttack()
    {
        swordCollider.enabled = false;
        //Debug.Log("sword collider disabled");
    }
}
