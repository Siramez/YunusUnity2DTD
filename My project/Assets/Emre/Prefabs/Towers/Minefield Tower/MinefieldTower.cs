using UnityEngine;

public class MinefieldTower : MonoBehaviour
{
    public GameObject bombPrefab;
    public float spawnRate = 2.0f;
    public float towerRadius = 5.0f;

    private bool isTowerDisabled = false;
    private float disabledTimeRemaining;

    private float nextSpawnTime;

    private void Update()
    {
        if (!isTowerDisabled && Time.time > nextSpawnTime)
        {
            SpawnBomb();
            nextSpawnTime = Time.time + spawnRate;
        }

        // Check if the tower is disabled
        if (isTowerDisabled)
        {
            disabledTimeRemaining -= Time.deltaTime;

            if (disabledTimeRemaining <= 0)
            {
                // Re-enable the tower when the disabled time is over
                isTowerDisabled = false;
            }
        }
    }

    void SpawnBomb()
    {
        // Generate a random position within the tower's radius
        Vector2 randomPosition = Random.insideUnitCircle * towerRadius;
        Vector3 spawnPosition = new Vector3(randomPosition.x, randomPosition.y, 0f);

        // Instantiate a bomb at the random position
        GameObject bombInstance = Instantiate(bombPrefab, transform.position + spawnPosition, Quaternion.identity);

        // Set the bomb's explosion parameters (you can adjust these values)
        Bomb bombScript = bombInstance.GetComponent<Bomb>();
        bombScript.explosionTime = 3.0f;
        bombScript.explosionRadius = 5.0f;
        bombScript.explosionDamage = 10;
    }

    public void DisableTower(float disableTime)
    {
        // Disable the tower for the specified time
        isTowerDisabled = true;
        disabledTimeRemaining = disableTime;
    }
}
