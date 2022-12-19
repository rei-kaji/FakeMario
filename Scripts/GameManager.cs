using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    //[SerializeField] GameObject gamaOverText;
    [SerializeField] TextMeshProUGUI gamaOverText;
    [SerializeField] TextMeshProUGUI gamaClearText;
    [SerializeField] TextMeshProUGUI scoreText;

    const int MAX_SCORE = 9999;
    int score = 0;

    public void AddScore(int val)
    {
        score += val;

        if(score > MAX_SCORE)
        {
            score = MAX_SCORE;
        }

        scoreText.text = score.ToString();
    }

    public void GameOver()
    {
        // Legacy version
        //gamaOverText.SetActive(true);

        // New version
        gamaOverText.enabled = true;
        Invoke("RestartScene", 1.5f);
        //RestartScene();
    }

    public void GameClear()
    {
        gamaClearText.enabled = true;
        Invoke("RestartScene", 1.5f);
    }

    void RestartScene()
    {
        Scene thisScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(thisScene.name);
    }
}
