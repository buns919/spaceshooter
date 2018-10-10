using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [Header("Particle")]
    [SerializeField] GameObject explosionParticlePrefab;
    [SerializeField] float durationOfExplosion = 1f;

    [Header("Laser")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float laserSpeed = 1f;

    // serialized for debug
    [Header("Debug")]
    [SerializeField] float health = 100;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = .5f;
    [SerializeField] float maxTimeBetweenShots = 3.5f;
    

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
        if (!damageDealer) {
            return;
        }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer) {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0) {
            Die();
        }
    }

    private void Die() {
        GameObject explosionParticles = Instantiate(
            explosionParticlePrefab,
            transform.position,
            Quaternion.identity
        );

        Destroy(gameObject);
        Destroy(explosionParticles, durationOfExplosion);
    }
}

