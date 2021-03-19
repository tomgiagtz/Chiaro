using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaveController : MonoSingleton<WaveController>
{
    public WaveList waveList;
    List<Wave> waves;
    public Wave currWave;
    public bool isSpawning, spawnsFinished;
    public GameObject winDisplay;
    List<GameObject> aliveEnemies = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        winDisplay.SetActive(false);
        waves = new List<Wave>(waveList.waves);
        foreach (Wave w in waves)
        {
            WaveErrors err = w.IsValid();
            if (err != WaveErrors.None) {
                Debug.LogWarning("Wave invalid: " + w);
                Debug.LogError("Wave error: " + err);
            }
        }

        currWave = waves[0];
        StartCoroutine("WaitForNextWave");
    }

    // Update is called once per frame
    void Update()
    {
        aliveEnemies.RemoveAll(item => item == null);
        if (waves.Count == 0 && spawnsFinished && aliveEnemies.Count == 0) {
            winDisplay.SetActive(true);
        }

        if (currWave != null && !spawnsFinished && isSpawning) {
            IsSpawning();
        }
    }

    float spawnCooldown = 0f;
    [HideInInspector]
    public int numEnemiesSpawned = 0;
    void IsSpawning() {
        if (spawnCooldown <= 0f) {
            if (numEnemiesSpawned < currWave.numberOfEnemies) {
                EnemyValues enemyValue = GetEnemyType();
                SpawnEnemy(enemyValue);
                spawnCooldown = 1f / currWave.spawnRate;
                numEnemiesSpawned++;
            } else {
                Debug.Log("NextWave");
                NextWave();
                numEnemiesSpawned = 0;
            }
        }
        spawnCooldown -= Time.deltaTime;
    }

    void SpawnEnemy(EnemyValues enemyValue) {
        aliveEnemies.Add(Instantiate(enemyValue.prefab, ValidPathController.Instance.spawnPoint.transform));
    }

    void NextWave() {
        StartCoroutine("WaitForNextWave");
        waves.RemoveAt(0);
        if (waves.Count > 0) {
            currWave = waves[0];
        } else {
            currWave = null;
            spawnsFinished = true;
        }
        
    }

    float timeBetweenWaves = 3f;
    IEnumerator WaitForNextWave() {
        isSpawning = false;
        yield return new WaitForSeconds(timeBetweenWaves);
        isSpawning = true;

    }

    EnemyValues GetEnemyType() {
        float rand = Random.Range(1f, 100f);
        bool typeFound = false;
        for (int i = 0; i < currWave.enemySpawnRates.Length && !typeFound; i++)
        {
            float currSpawnRate = currWave.enemySpawnRates[i];
            if (rand >= currSpawnRate){
                rand -= currSpawnRate;
            } else {
                Debug.Log("Spawning: "+ currWave.enemyTypes[i]);
                Debug.Log("Wave: "+ currWave);
                return currWave.enemyTypes[i];
            }
        }
        Debug.LogWarning("Enemy Type Not Found" + currWave);
        return currWave.enemyTypes[0];
    }


}
