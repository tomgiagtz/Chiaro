using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WaveController : MonoSingleton<WaveController>
{
    //set in inspector
    public WaveList waveListData;
    public Scrollbar waveScroll;


    //data
    public List<Wave> waves;
    public Wave currWave;
    public bool isSpawning, spawnsFinished, hasWinCondition, allWavesSpawned;
    public GameObject winDisplay;
    List<GameObject> aliveEnemies = new List<GameObject>();

    WaveDisplayController waveDisplay;
    // Start is called before the first frame update
    void Start()
    {
        winDisplay.SetActive(false);
        waves = new List<Wave>(waveListData.waves);
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
            hasWinCondition = true;
        }

        if (currWave != null && !spawnsFinished && isSpawning) {
            IsSpawning();
        }
    }

    float spawnCooldown = 0f;
    public float waveProgress = 0f;
    [HideInInspector]
    public int numEnemiesSpawned = 0;
    void IsSpawning() {
        if (spawnCooldown <= 0f) {
            if (numEnemiesSpawned < currWave.numberOfEnemies) {
                EnemyValues enemyValue = GetEnemyType();
                SpawnEnemy(enemyValue);
                spawnCooldown = 1f / currWave.spawnRate;
                numEnemiesSpawned++;
                waveProgress = (float) numEnemiesSpawned / currWave.numberOfEnemies;
            } else {
                Debug.Log("NextWave");
                waveProgress = 1f;
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
        waveProgress = 0f;

        WaveDisplayController.Instance.OnWaveComplete();
        waves.RemoveAt(0);
        if (waves.Count > 0) {
            currWave = waves[0];
        } else {
            currWave = null;
            spawnsFinished = true;
        }
        
    }


    //should wait for last enemy to die
    public float timeBetweenWaves = 5f;
    IEnumerator WaitForNextWave() {
        isSpawning = false;
        yield return new WaitUntil(() => aliveEnemies.Count == 0);
        yield return new WaitForSeconds(timeBetweenWaves);
        //start next wave
        isSpawning = true;
        waveProgress = 0f;

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
            //yrdst
        }
        Debug.LogWarning("Enemy Type Not Found" + currWave);
        return currWave.enemyTypes[0];
    }


    // void InitWaveDisplay() {
    //     waveDisplay = gameObject.AddComponent<WaveDisplayController>();
    //     waveDisplay.waves = waves;
    //     waveDisplay.waveScroll = waveScroll;
    // }



}
