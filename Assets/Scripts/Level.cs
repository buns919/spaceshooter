using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Level : MonoBehaviour {

    bool playerIsAlive = true;

    public bool GetPlayerIsAlive() { return playerIsAlive; }
    public void SetPlayerIsAlive(bool isAlive) { playerIsAlive = isAlive; }

    public void LoadGame() {
        SceneManager.LoadScene("Game");
    }

    public void LoadStartMenu() {
        SceneManager.LoadScene("StartMenu");
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void LoadGameOver() {
        StartCoroutine(DelayLoadGameOver());
    }

    IEnumerator DelayLoadGameOver() {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("GameOver");
    }
}
