using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] public float health,hunger,thirst,score; // Show in UI
    [SerializeField] private float maxHealth, maxHunger, maxThirst, hungerDt = 0.02f, thirstDt = 0.02f;
    [SerializeField] private bool isDead = false;
    [SerializeField] public bool isSlotFull = false;

    // Start is called before the first frame update
    void Start()
    {
        
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

    public void AddScore(float g_score)
    {
        score = score + g_score;
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
        hunger -= hungerDt * Time.deltaTime;
        thirst -= thirstDt * Time.deltaTime;

        if (hunger <= 0)
        {
            hunger = 0;
            TakeDamage(hungerDt * Time.deltaTime);
        }

        if (thirst <= 0)
        {
            thirst = 0;
            TakeDamage(thirstDt * Time.deltaTime);
        }

    }
}
