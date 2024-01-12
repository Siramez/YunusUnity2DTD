using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveSpawner : MonoBehaviour
{
    [SerializeField] public Transform[] EnemyPaths;
    [SerializeField] private float spawnDelay = 5f;
    [SerializeField] private GameObject[] EnemysPrefabs;
    private float spawnDelayTimer;
    [SerializeField] private float delayStartTimer;

    [SerializeField] private float timeBetweenWaves = 10f;
    [SerializeField] private int initialWaveCount = 1;
    [SerializeField] private int waveCountIncrease = 1;

    private int currentWave = 0;
    private int enemiesToDefeatForNextWave = 10; // Adjust this value as needed
    private int enemiesDefeated = 0;
    internal void EnemyDefeated()
    {
        enemiesDefeated++;
    }

    void Start()
    {
        StartCoroutine(StartWaves());
    }

    IEnumerator StartWaves()
    {
        yield return new WaitForSeconds(2f); // Initial delay before the first wave

        while (currentWave < initialWaveCount)
        {
            Debug.Log("Wave " + currentWave + " started!");

            // Spawn enemies for the current wave
            SpawnWaveEnemies(currentWave);

            // Wait for all enemies to be defeated
            yield return new WaitUntil(() => AreAllEnemiesDefeated());

            // Wait for the time between waves
            yield return new WaitForSeconds(timeBetweenWaves);

            currentWave++; // Increment the wave number here
            enemiesToDefeatForNextWave = initialWaveCount + currentWave * waveCountIncrease;
        }
    }

    void SpawnWaveEnemies(int waveNumber)
    {
        // Calculate the number of enemies for the current wave (you can customize this logic)
        int enemyCount = initialWaveCount + waveNumber * waveCountIncrease;

        // Spawn enemies
        SpawnEnemies(enemyCount);
    }

    void Update()
    {
        spawnDelayTimer -= Time.unscaledDeltaTime;
        delayStartTimer -= Time.deltaTime;
        if (spawnDelayTimer <= 0f && delayStartTimer <= 0)
        {
            SpawnEnemy();
            spawnDelayTimer = spawnDelay;
        }
    }

    void SpawnEnemy()
    {
        System.Random random = new System.Random();
        int randomEnemy = random.Next(0, EnemysPrefabs.Length);
        GameObject newEnemy = Instantiate(EnemysPrefabs[randomEnemy], EnemyPaths[0].position, Quaternion.identity);

        // Use the property to set waypoints
        IDamageable[] damageables = newEnemy.GetComponentsInChildren<IDamageable>();
        HordeZombielingsMovement movementScript = newEnemy.GetComponent<HordeZombielingsMovement>();

        foreach (IDamageable damageable in damageables)
        {
            // Handle each damageable component if needed
        }
    }

    public void SpawnEnemies(int enemyCount)
    {
        enemiesToDefeatForNextWave = enemyCount; // Set the target number of enemies for the next wave
        for (int i = 0; i < enemyCount; i++)
        {
            SpawnEnemy();
        }
    }

    public void OnDrawGizmos()
    {
        if (EnemyPaths != null)
            for (int i = 0; i < EnemyPaths.Length - 1; i++)
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawLine(EnemyPaths[i].position, EnemyPaths[i + 1].position);
            }
    }

    internal bool AreAllEnemiesDefeated()
    {
        // Check if the required number of enemies for the current wave have been defeated
        if (enemiesDefeated >= enemiesToDefeatForNextWave)
        {
            enemiesDefeated = 0; // Reset the count for the next wave
            return true;
        }

        // Return false if the target number of enemies is not reached yet
        return false;
    }
}
