using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour {

    TextMeshProUGUI textMesh;
    GameSession gameSession;

	// Use this for initialization
	void Start () {
        textMesh = GetComponent<TextMeshProUGUI>();
        gameSession = FindObjectOfType<GameSession>();
	}
	
	// Update is called once per frame
	void Update () {
        textMesh.SetText(gameSession.GetScore().ToString());
    }
}
