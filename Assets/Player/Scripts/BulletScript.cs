using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private LayerMask whatIsEnemies;

    //LifeTime
    [SerializeField] private int maxCollisions;
    [SerializeField] private float maxLifeTime;
    [SerializeField] private float destroyDelay;
    [SerializeField] private float damage;
    [SerializeField] private bool onTouch =true;

    [SerializeField] private int collisions;
    PhysicMaterial physics_mat;

    private void Delay()
    {
        Invoke("Destroy", destroyDelay);
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (collisions > maxCollisions) Delay();

        maxLifeTime -= Time.deltaTime;
        if (maxLifeTime <= 0) Delay();
    }

    private void OnCollisionEnter(Collision collision)
    {
        collisions++; //Count up collisions

        if (collision.collider.CompareTag("Enemy") && onTouch)
        {
            collision.gameObject.GetComponent<EnemyStats>().TakeDamage(damage);
            Debug.Log("Hit enemy");
            Delay(); // Destroy if bullet hits an enemy

        }

        else
        if (collision.collider.CompareTag("Player") && onTouch)
        {
            collision.gameObject.GetComponent<PlayerStats>().TakeDamage(damage);
            Debug.Log("Hit player");
            Delay(); // Destroy if bullet hits an enemy

        }
    }
}
