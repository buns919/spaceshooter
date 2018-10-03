using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] GameObject laserPrefab;
    [SerializeField] float laserSpeed = 1f;

    // serialized for debug
    [SerializeField] float health = 100;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = .2f;
    [SerializeField] float maxTimeBetweenShots = 2.5f;
    

    // Use this for initialization
    void Start() {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    // Update is called once per frame
    void Update() {
        CountDownAndShoot();
    }

    private void CountDownAndShoot() {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f) {
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot() {
        while (true) {
            GameObject laserInstance = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
            laserInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -laserSpeed);
            yield return new WaitForSeconds(shotCounter);
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer) {
        health -= damageDealer.GetDamage();
        if (health <= 0) {
            Destroy(gameObject);
        }
    }
}

