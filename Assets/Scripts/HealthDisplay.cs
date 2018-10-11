using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDisplay : MonoBehaviour {

    Player player;
    SpriteRenderer[] healthSprites;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<Player>();
        healthSprites = GetComponentsInChildren<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        int health = player.GetHealth();
        if (health == 3) {
            return;
        }

        if (health == 2) {
            Destroy(healthSprites[2]);
        }

        if (health == 1) {
            Destroy(healthSprites[1]);
        }
    }
}
