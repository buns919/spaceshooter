using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField] List<WaveConfig> waveConfigs;

    int startingWave = 0;


	// Use this for initialization
	void Start () {
        var currentWave = waveConfigs[startingWave];

        StartCoroutine(SpawnAllEnemiesInWave(currentWave));
	}

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig) {
        var numEnemiesToSpawn = waveConfig.GetNumEnemies();

        for (int i = 0; i < numEnemiesToSpawn; i++) {
            Instantiate(
                waveConfig.GetEnemyPrefab(),
                waveConfig.GetWaypoints()[0].transform.position,
                Quaternion.identity
            );

            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
    }
}
