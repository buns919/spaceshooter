﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [Header("Player")]
    [SerializeField] float horizontalMoveSpeed = 10f;
    [SerializeField] float verticalMoveSpeed = 11f;
    [SerializeField] float padding = 1f;
    [SerializeField] int health = 3;

    [Header("Sounds")]
    [SerializeField] AudioClip playerDeathSound;
    [SerializeField] float playerDeathSoundVolume = .7f;
    [SerializeField] AudioClip playerLaserSound;
    [SerializeField] float playerLaserSoundVolume = .5f;

    [Header("Laser")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float laserSpeed = 24f;
    [SerializeField] float projectileFiringPeriod = 0.2f;

    Coroutine firingCoroutine;

    float xMin;
    float xMax;
    float yMin;
    float yMax;
    Level level;

    // Use this for initialization
    void Start () {
        level = FindObjectOfType<Level>();
        SetupMoveBoundaries();
	}

    // Update is called once per frame
    void Update () {
        Move();
        Fire();
	}

    private void Move() {
        // make the game framerate independent
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * horizontalMoveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * verticalMoveSpeed;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);
    }

    void Fire() {
        if (Input.GetButtonDown("Fire1")) {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        if (Input.GetButtonUp("Fire1")) {
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator FireContinuously() {
        while (true) {
            GameObject laserInstance = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
            laserInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);
            AudioSource.PlayClipAtPoint(playerLaserSound, Camera.main.transform.position, playerLaserSoundVolume);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }

    /**
     * map world coordinate to 0 (left side) - 1 (right side), 0 (bottom) - 1 (top)
     */
    void SetupMoveBoundaries() {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
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
        AudioSource.PlayClipAtPoint(playerDeathSound, Camera.main.transform.position, playerDeathSoundVolume);
        level.LoadGameOver();
        Destroy(gameObject);
        level.SetPlayerIsAlive(false);
    }

    public int GetHealth() {
        return health;
    }
}
