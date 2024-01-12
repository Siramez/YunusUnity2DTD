using UnityEngine;

public class PlayerHealthManager : MonoBehaviour, IDamageable
{
    public static PlayerHealthManager Instance { get; private set; } // Singleton instance

    public int maxHealth = 10;
    private int currentHealth;
    [SerializeField] private SpriteRenderer healthBar;
    [SerializeField] private SpriteRenderer healthFill;

    // Add a layer mask to ignore bullets fired by towers
    public LayerMask bulletIgnoreLayer;

    void Start()
    {
        currentHealth = maxHealth;
        Instance = this; // Set the singleton instance
        currentHealth = maxHealth;
        healthFill.size = healthBar.size;

        Debug.Log("PlayerHealthManager initialized");
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            HandleDeath();
        }

        float healthPercentage = (float)currentHealth / maxHealth;
        healthFill.size = new Vector2(healthPercentage * healthBar.size.x, healthBar.size.y);
    }

    public void NotifyDefeated()
    {
        // Implement the logic if needed
    }

    public void SlowDown()
    {
        // Implement the slowdown logic if needed
    }

    public bool IsAlive
    {
        get { return currentHealth > 0; }
    }

    void HandleDeath()
    {
        // Implement the logic for player death if needed
    }

    // Ignore collisions with bullets fired by towers
    void OnTriggerEnter2D(Collider2D other)
    {
        // Example: If the collided object has the "Enemy" tag, take damage
        if (other.CompareTag("Enemy"))
        {
            // Get the IDamageable component from the collided object
            IDamageable enemyDamageable = other.GetComponent<IDamageable>();

            // Deal damage to the enemy
            if (enemyDamageable != null)
            {
                enemyDamageable.TakeDamage(1); // Adjust the damage value as needed
            }
        }
    }
}
