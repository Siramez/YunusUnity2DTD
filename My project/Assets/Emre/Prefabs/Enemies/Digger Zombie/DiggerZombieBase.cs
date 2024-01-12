using System.Collections;
using UnityEngine;

public class DiggerZombieBase : MonoBehaviour, IDamageable
{
    [SerializeField] public int maxHealth = 1;
    [SerializeField] private SpriteRenderer healthBar;
    [SerializeField] private SpriteRenderer healthFill;
    [SerializeField] private int GoldValue = 10;

    private int currentHealth;
    public float speed = 2.0f;
    public int currentWaypoint = 0;
    public int damageToPlayer = 1;
    public Transform[] waypoints;
    public Transform[] Waypoints
    {
        get { return waypoints; }
        set { waypoints = value; }
    }
    public bool IsAlive
    {
        get { return currentHealth > 0; }
    }
    public void NotifyDefeated()
    {
        // Increment the enemiesDefeated counter in EnemyWaveSpawner
        FindObjectOfType<EnemyWaveSpawner>().EnemyDefeated();
    }

    void Start()
    {
        currentHealth = maxHealth;
        healthFill.size = healthBar.size;

        if (waypoints == null || waypoints.Length == 0)
        {
            FindWaypoints();
        }
    }
    void FindWaypoints()
    {
        GameObject spawner = GameObject.Find("Spawner");

        if (spawner != null)
        {
            waypoints = spawner.GetComponent<EnemyWaveSpawner>().EnemyPaths;
        }
    }

    public void SetWaypoints(Transform[] newWaypoints, int newCurrentWaypoint)
    {
        waypoints = newWaypoints;
        currentWaypoint = newCurrentWaypoint;
    }



    void Update()
    {
        if (waypoints == null || waypoints.Length == 0)
            return;

        MoveTowardsWaypoint();

        if (Vector3.Distance(transform.position, waypoints[currentWaypoint].position) < 0.1f)
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
        }
        if (currentHealth <= 0)
        {
            HandleDeath();
        }

        DealDamageToPlayer();
    }
    void DealDamageToPlayer()
    {
        if (Vector3.Distance(transform.position, PlayerHealthManager.Instance.transform.position) < 1.0f)
        {
            PlayerHealthManager.Instance.TakeDamage(damageToPlayer);
        }
    }
    void MoveTowardsWaypoint()
    {
        Vector3 direction = waypoints[currentWaypoint].position - transform.position;
        direction.Normalize();

        transform.Translate(direction * speed * Time.deltaTime);
    }
    public void SetTargetPosition(Vector3 targetPosition)
    {
        healthBar.transform.parent = null;
        healthBar.transform.parent = transform;
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

        LevelManager.Instance.PlayerTakeDamage(damage);
    }

    void HandleDeath()
    {
        LevelManager.Instance.Gold += GoldValue;
        LevelManager.Instance.EnemyLeft--;

        // Check if the player is still alive before dealing damage
        if (IsCollidingWithPlayer())
        {
            PlayerHealthManager.Instance.TakeDamage(damageToPlayer);
        }

        NotifyDefeated();
        Destroy(gameObject);
    }
    bool IsCollidingWithPlayer()
    {
        // Check if there are any colliders between the enemy and the player
        RaycastHit2D hit = Physics2D.Raycast(transform.position, PlayerHealthManager.Instance.transform.position - transform.position, 1.0f);

        // If the hit.collider is not null and has the "Player" tag, return true
        return hit.collider != null && hit.collider.CompareTag("Player");
    }

    protected virtual IEnumerator Slow()
    {
        speed = speed / 2;
        yield return new WaitForSeconds(2.0f);
        speed = speed * 2;
    }

    protected virtual void OnDeath()
    {
        LevelManager.Instance.Gold += GoldValue;
        LevelManager.Instance.EnemyLeft--;
        currentHealth = 0;
        Destroy(gameObject);
    }

    public void SlowDown()
    {
        StartCoroutine(Slow());
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collision Detected");
        // Check if the collided object has the "Player" tag
        if (other.CompareTag("Player"))
        {
            // Get the IDamageable component from the collided object
            IDamageable playerDamageable = other.GetComponent<IDamageable>();

            // Deal damage to the player
            if (playerDamageable != null)
            {
                playerDamageable.TakeDamage(damageToPlayer); // Adjust the damage value as needed
                Destroy(gameObject); // Destroy the enemy after dealing damage to the player
            }
        }
    }
}
