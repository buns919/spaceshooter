using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour {

    private int score = 0;

    private void Awake() {
        SetUpSingleton();
    }

    private void SetUpSingleton() {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1) {
            Destroy(gameObject);
        }
        else {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void AddToScore(int scoreValue) {
        score += scoreValue;
    }

    public int GetScore() { return score; }

    public void ResetGame() {
        Destroy(gameObject);
    }
}
