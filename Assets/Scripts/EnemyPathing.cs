using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour {

    [SerializeField] WaveConfig waveConfig;

    List<Transform> enemyWaypoints;
    [SerializeField] float enemyMoveSpeed = 5f;

    int waypointIndex = 0;

	// Use this for initialization
	void Start () {
        enemyWaypoints = waveConfig.GetWaypoints();
        // start at position 0
        transform.position = enemyWaypoints[waypointIndex].transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        Move();
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
