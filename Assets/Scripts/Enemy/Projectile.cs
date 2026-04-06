using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float speed = 20f;
    public float lifeTime = 5f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * speed, ForceMode.VelocityChange);

        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public void OnCollisionEnter(Collision collision)
    //{
    //    Destroy(gameObject);
    //}
}
