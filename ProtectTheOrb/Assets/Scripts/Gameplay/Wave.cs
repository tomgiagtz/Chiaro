using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public enum WaveTypes {
    Standard,
    Fast,
    Flying
}

public enum WaveErrors {
    None,
    ArrayLengthMismatch,
    InvalidSpawnRateSum
}
[CreateAssetMenu(fileName = "Wave", menuName="Values/Wave")]
public class Wave : ScriptableObject
{
    public WaveTypes waveType;
    public int numberOfEnemies;
    public List<EnemyValues> enemyTypes = new List<EnemyValues>();
    public float[] enemySpawnRates;
    //numEnemies per second
    public float spawnRate = 1;

    public WaveErrors IsValid() {
        float sumSpawnRates = 0;
        foreach (var sr in enemySpawnRates)
        {
            sumSpawnRates += sr;
        }
        if (enemyTypes.Count != enemySpawnRates.Length) {
            return WaveErrors.ArrayLengthMismatch;
        } 
        if (sumSpawnRates != 100f) {
            return WaveErrors.InvalidSpawnRateSum;
        }

        return WaveErrors.None;
    }
    
}