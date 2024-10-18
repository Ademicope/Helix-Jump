using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    //[SerializeField]
    //private TextMeshProUGUI highScore;
    [SerializeField]
    private TMP_InputField username;

    public int highscore;
    //private Leaderboard leaderboard;
    private GameManager gameManager;
    public UnityEvent<string, int> submitScoreEvent;

    private void Awake()
    {
        //leaderboard = FindObjectOfType<Leaderboard>(true);
        gameManager = FindObjectOfType<GameManager>();

        /*if (leaderboard == null)
        {
            Debug.LogError("Leaderboard component not found! Please make sure it's attached to a GameObject in the scene.");
        }*/

        if (gameManager == null)
        {
            Debug.LogError("GameManager component not found! Please make sure it's attached to a GameObject in the scene.");
        }
    }
    public void SubmitScore()
    {
        highscore = gameManager.bestScore;
        submitScoreEvent.Invoke(username.text, highscore);
        
        /*if (leaderboard != null && gameManager != null)
        {
            highscore = gameManager.score;
            leaderboard.SetLeaderboardEntry(username.text, highscore);
        }
        else
        {
            Debug.LogError("Leaderboard or GameManager is missing, cannot submit score.");
        }*/
    }
}
