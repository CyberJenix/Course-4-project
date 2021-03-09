using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] public float health,hunger,thirst;
    [SerializeField] private float maxHealth, maxHunger, maxThirst, hungerDt =0.02f, thirstDt=0.02f;
    [SerializeField] private bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            isDead = true;
        }
    }

    public void Heal(float heal)
    {
        health += heal;
        if (health >maxHealth)
        {
            health = maxHealth;
        }
    }

    public void Eat(float food)
    {
        hunger += food;
        if (hunger > maxHunger)
        {
            hunger = maxHunger;
        }
    }

    public void Drink(float water)
    {
        thirst += water;
        if (thirst > maxThirst)
        {
            thirst = maxThirst;
        }
    }

    // Update is called once per frame
    void Update()
    {
        hunger = hunger - hungerDt * Time.deltaTime;
        thirst = thirst - thirstDt * Time.deltaTime;
    }
}
