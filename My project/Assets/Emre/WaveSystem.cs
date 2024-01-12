using System.Collections;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    [SerializeField] private Spawner enemySpawner;
    [SerializeField] private float timeBetweenWaves = 10f;
    [SerializeField] private int initialWaveCount = 1;
    [SerializeField] private int waveCountIncrease = 1;

    private int currentWave = 0;

    void Start()
    {
        StartCoroutine(StartWaves());
    }

    IEnumerator StartWaves()
    {
        yield return new WaitForSeconds(2f); // Initial delay before the first wave

        while (true)
        {
            currentWave++;
            Debug.Log("Wave " + currentWave + " started!");

            // Spawn enemies for the current wave
            SpawnWaveEnemies(currentWave);

            // Wait for all enemies to be defeated
            yield return new WaitUntil(() => enemySpawner.AreAllEnemiesDefeated());

            // Wait for the time between waves
            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }

    void SpawnWaveEnemies(int waveNumber)
    {
        // Calculate the number of enemies for the current wave (you can customize this logic)
        int enemyCount = initialWaveCount + (waveNumber - 1) * waveCountIncrease;

        // Spawn enemies
        enemySpawner.SpawnEnemies(enemyCount);
    }
}
