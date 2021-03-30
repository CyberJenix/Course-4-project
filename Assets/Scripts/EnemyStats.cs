using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] private float health, score;
    [SerializeField] internal bool isDead = false;
    [SerializeField] private bool scoreAdded = false;
    [SerializeField] PlayerStats playerstats;
    // Start is called before the first frame update
    void Start()
    {
       // playerstats = gameObject.GetComponent<PlayerStats>();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            isDead = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead && !scoreAdded)
        {
            playerstats.AddScore(score);
            scoreAdded = true;
        }
}
}
