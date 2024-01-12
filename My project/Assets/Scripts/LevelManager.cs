using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance = null;
    public int Gold = 100;
    public int LivesLeft = 5;
    public int EnemyLeft = 20;
    public int NextLevel;
    public int CurrentWaveNumber { get; set; } = 0;
    public int TotalWaves = 5;
    public float WaveDelay = 5f;
    public static LevelManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<LevelManager>();
            }
            return instance;
        }
    }

    [SerializeField] private Transform towerUIParent;
    [SerializeField] private GameObject towerUIPrefab;
    [SerializeField] private Tower[] towerPrefabs;

    private List<Tower> spawnedTowers = new List<Tower>();

    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Text statusInfo;
    [SerializeField] private TMP_Text LivesText;
    [SerializeField] private TMP_Text TotalEnemyText;
    [SerializeField] private TMP_Text GoldText;
    [SerializeField] private TMP_Text WaveInfoText;

    private int _currentLives;
    private int _enemyCounter;

    // Start is called before the first frame update
    private void Start()
    {
        InstantiateAllTowerUI();
        StartCoroutine(StartWaves());
        CurrentWaveNumber = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        GoldText.text = "Gold: " + Gold.ToString();
        LivesText.text = "Lives: " + LivesLeft.ToString();
        TotalEnemyText.text = "Enemies: " + EnemyLeft.ToString();
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (LivesLeft <= 0)
        {
            SetGameOver(false);
        }
    }

    private void InstantiateAllTowerUI()
    {
        foreach (Tower tower in towerPrefabs)
        {
            GameObject newTowerUIObj = Instantiate(towerUIPrefab.gameObject, towerUIParent);
            TowerUI newTowerUI = newTowerUIObj.GetComponent<TowerUI>();
            newTowerUI.SetTowerPrefab(tower);
            newTowerUI.transform.name = tower.name;
        }
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(NextLevel);
    }

    public void PlayerTakeDamage(int damage)
    {
        Debug.Log("PlayerTakeDamage - Start: LivesLeft = " + LivesLeft);
        LivesLeft -= damage;
        UpdateLivesUI();
        Debug.Log("PlayerTakeDamage - End: LivesLeft = " + LivesLeft);

        Debug.Log("PlayerTakeDamage called. Current Lives: " + LivesLeft);
    }

    private void UpdateLivesUI()
    {
        Debug.Log("UpdateLivesUI - Start: LivesLeft = " + LivesLeft);
        LivesText.text = "Lives: " + LivesLeft.ToString();

        if (LivesLeft <= 0)
        {
            SetGameOver(false);
        }

        Debug.Log("UpdateLivesUI - End: LivesLeft = " + LivesLeft);
    }

    private IEnumerator StartWaves()
    {
        while (CurrentWaveNumber < TotalWaves)
        {
            yield return StartCoroutine(StartWave());
            // Additional delay between waves if needed
            yield return new WaitForSeconds(WaveDelay);
            CurrentWaveNumber++;  // Increment the wave number here
        }
    }

    private IEnumerator StartWave()
    {
        {
            WaveInfoText.text = "Wave " + CurrentWaveNumber.ToString();
            yield return new WaitForSeconds(WaveDelay);
            // Additional logic for starting the wave
        }

    }

    public void SetGameOver(bool isWin)
    {
        statusInfo.text = isWin ? "You Win!" : "You Lose!";
        panel.gameObject.SetActive(true);
    }
}
