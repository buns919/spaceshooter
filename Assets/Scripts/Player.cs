using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] float horizontalMoveSpeed = 10f;
    [SerializeField] float verticalMoveSpeed = 15f;
    [SerializeField] float padding = 1f;

    float xMin;
    float xMax;
    float yMin;
    float yMax;

    // Use this for initialization
    void Start () {
        SetupMoveBoundaries();
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

    // Update is called once per frame
    void Update () {
        Move();
	}

    private void Move() {
        // make the game framerate independant
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * horizontalMoveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * verticalMoveSpeed;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);
    }
}
