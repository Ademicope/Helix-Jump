using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int bestScore;
    public int score;

    public int currentStage = 0;

    public static GameManager singleton;

    // Start is called before the first frame update
    void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }
        else if (singleton == this)
        {
            Destroy(gameObject);
        }

        bestScore = PlayerPrefs.GetInt("Highscore ");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextLevel()
    {
        currentStage++;
        FindObjectOfType<BallController>().ResetBall();
        FindObjectOfType<HelixController>().LoadStage(currentStage);

        Debug.Log("Next level called");
    }

    public void RestartLevel()
    {
        Debug.Log("Game Over");
        //Show ads
        singleton.score = 0;
        FindObjectOfType<BallController>().ResetBall();
        // Reload stage
        FindObjectOfType<HelixController>().LoadStage(currentStage);
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;

        if (score > bestScore)
        {
            bestScore = score;
            // store highscore/ best score in player prefs
            PlayerPrefs.SetInt("Highscore", bestScore);
        }
    }
}
