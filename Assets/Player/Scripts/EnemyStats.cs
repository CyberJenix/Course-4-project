using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private bool isDead = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        {
            if (health <= 0)
            {
                isDead = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
