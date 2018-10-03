using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour {

    float enemyMoveSpeed;
    List<Transform> enemyWaypoints;
    WaveConfig waveConfig;

    int waypointIndex = 0;

	// Use this for initialization
	void Start () {
        enemyWaypoints = waveConfig.GetWaypoints();
        enemyMoveSpeed = waveConfig.GetMoveSpeed();
        // start at position 0
        transform.position = enemyWaypoints[waypointIndex].transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        Move();
    }

    public void SetWaveConfig(WaveConfig waveConfig) {
        this.waveConfig = waveConfig;
    }

    private void Move() {
        if (waypointIndex <= enemyWaypoints.Count - 1) {
            var targetPosition = enemyWaypoints[waypointIndex].transform.position;
            var movementThisFrame = enemyMoveSpeed * Time.deltaTime;

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);

            if (transform.position == targetPosition) {
                waypointIndex++;
            }
        }
        else {
            Destroy(gameObject);
        }
    }
}
