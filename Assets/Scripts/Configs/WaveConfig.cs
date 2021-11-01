using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] private GameObject[] enemiesPrefab;
    [SerializeField] private GameObject pathPrefab;
    [SerializeField] private float timeBetweenSpawns = 0.5f;
    [SerializeField] private float spawnRandomFactor = 0.3f;
    [SerializeField] private int numberOfEnemies = 10;
    [SerializeField] private float moveSpeed = 2f;

    public GameObject GetRandomEnemyPrefab() => enemiesPrefab[Random.Range(0, enemiesPrefab.Length)];

    public List<Transform> GetWaypoints()
    {
        List<Transform> waypoints = new List<Transform>();
        foreach (Transform transform in pathPrefab.transform)
        {
            waypoints.Add(transform);
        }
        
        return waypoints;
    }

    public float GetTimeBetweenSpawns() => timeBetweenSpawns;
    
    public float GetSpawnRandomFactor() => spawnRandomFactor;
    
    public int GetNumberOfEnemies() => numberOfEnemies;
    
    public float GetMoveSpeed() => moveSpeed;
}
