using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject {

    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float timeBetweenSpawns = .5f;
    [SerializeField] float spawnTimeRandomFactor = .3f;
    [SerializeField] int numEnemies = 5;
    [SerializeField] float moveSpeed = 2f;

    public GameObject GetEnemyPrefab() { return enemyPrefab; }

    public List<Transform> GetWaypoints() {
        var waveWaypoints = new List<Transform>();

        foreach(Transform waypoint in pathPrefab.transform) {
            waveWaypoints.Add(waypoint);
        }

        return waveWaypoints;
    }

    public float GetTimeBetweenSpawns() { return timeBetweenSpawns; }

    public float GetSpawnTimeRandomFactor() { return spawnTimeRandomFactor; }

    public int GetNumEnemies() { return numEnemies; }

    public float GetMoveSpeed() { return moveSpeed; }
}
