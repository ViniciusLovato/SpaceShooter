using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Entities;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<WaveConfig> waveConfigs;

    private int startingWave = 0;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        var currentWave = waveConfigs[startingWave];
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        } while (true);
    }

    private IEnumerator SpawnAllWaves()
    {
        return waveConfigs.Select(waveConfig => StartCoroutine(SpawnAllEnemiesInWave(waveConfig))).GetEnumerator();
    }
    
    private IEnumerator SpawnAllEnemiesInWave(WaveConfig currentWave)
    {
        Transform currentWaypoints = currentWave.GetWaypoints()[0].transform;
        
        for (var i = 0; i < currentWave.GetNumberOfEnemies(); i++)
        {
            var enemy = Instantiate(currentWave.GetRandomEnemyPrefab(), currentWaypoints.position, Quaternion.identity);
            enemy.GetComponent<EnemyPathing>().Init(currentWave);
            
            yield return new WaitForSeconds(currentWave.GetTimeBetweenSpawns());
        }
    }

}
