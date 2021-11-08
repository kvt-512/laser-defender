using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    int score = 0;

    private void Awake()
    {
        Screen.SetResolution(1080,1920,true);
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        int numberGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numberGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    
    public int GetScore() {
        return score;
    }

    public void AddScore(int scoreValue) {
        score = score + scoreValue;
    }

    public void ResetScore() {
        Destroy(gameObject);
    }
}
