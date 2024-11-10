using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int bestScore;
    public int currentScore = 0;

    public int currentLevel = 0;

    public static GameManager singleton;

    public AudioSource winAudio;

    public Text textPlus1;

    void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }
        else if (singleton != this)
        {
            Destroy(gameObject);
        }

        bestScore = PlayerPrefs.GetInt("HighScore", currentScore);
    }

    public void NextLevel()
    {
        winAudio.Play();

        currentLevel++;
        FindObjectOfType<BallController>().ResetBall();
        FindObjectOfType<HelixContoller>().LoadStage(currentLevel);
        Debug.Log("Pasaste de nivel");
    }

    public void RestartLevel()
    {
        //Debug.Log("Restart");
        singleton.currentScore = 0;
        FindObjectOfType<BallController>().ResetBall();
        FindObjectOfType<HelixContoller>().LoadStage(currentLevel);
    }

    public void AddScore(int scoreToAdd)
    {
        textPlus1.GetComponent<Animation>().Play();

        currentScore += scoreToAdd;

        if (currentScore > bestScore)
        {
            bestScore = currentScore;
            PlayerPrefs.SetInt("HighScore", currentScore);
        }
    }
}