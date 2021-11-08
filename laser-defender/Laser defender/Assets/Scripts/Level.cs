using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    float delayTime = 2F;
    GameSession gameSession;
    public void LoadGameMenu() {
        SceneManager.LoadScene(0);
    }

    public void LoadCrashCourse() {
        SceneManager.LoadScene("Crash Course");
    }

    public void LoadGame() {
        StartCoroutine(DelayLoadGame());
    }

    IEnumerator DelayLoadGame() {
        yield return new WaitForSeconds(delayTime - 1);
        SceneManager.LoadScene("Game");
        gameSession = FindObjectOfType<GameSession>();
        gameSession.ResetScore();
    }

    public void LoadGameOver() {
        StartCoroutine(DealyGameOver());
    }

    IEnumerator DealyGameOver() {
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene("Game Over");
    }

    public void QuitGame() {
        Application.Quit();
    }
}
