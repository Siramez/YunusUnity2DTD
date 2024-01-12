using System.Collections;
using UnityEngine;

public class CamouflagedZombie : MonoBehaviour
{
    [SerializeField] public int maxHealth = 1;
    [SerializeField] private SpriteRenderer healthBar;
    [SerializeField] private SpriteRenderer healthFill;
    [SerializeField] private int GoldValue = 10;

    private int currentHealth;
    public float speed = 2.0f;

    void Start()
    {
        currentHealth = maxHealth;
        healthFill.size = healthBar.size;
    }

    

    void Update()
    {
        if (currentHealth <= 0)
        {
            HandleDeath();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            HandleDeath();
        }
    }

    void HandleDeath()
    {
        LevelManager.Instance.Gold += GoldValue;
        LevelManager.Instance.EnemyLeft--;
        currentHealth = 0;
        Destroy(gameObject);
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
}
