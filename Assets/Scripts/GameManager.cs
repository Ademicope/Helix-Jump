using System.Collections;
using System.Collections.Generic;
using UnityEditor.Advertisements;
using UnityEngine;
using UnityEngine.Advertisements;

public class GameManager : MonoBehaviour, IUnityAdsInitializationListener
{
    public int bestScore;
    public int score;

    public int currentStage = 0;

    public static GameManager singleton;

    private HelixController helixController;
    private const string gameId = "5714049";

    // Start is called before the first frame update
    void Awake()
    {
        helixController = FindObjectOfType<HelixController>();

        if (singleton == null)
        {
            singleton = this;
            InitializeAds();
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

    public void InitializeAds()
    {
        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(gameId, true, this);
        }
        else
        {
            Debug.Log("Ads already initialised");
        }
    }

    public void NextLevel()
    {
        currentStage++;
        if (currentStage == (helixController.allStages.Count))
        {
            currentStage = 0;
        }
        FindObjectOfType<BallController>().ResetBall();
        FindObjectOfType<HelixController>().LoadStage(currentStage);

        Debug.Log("Next level called");
    }

    public void RestartLevel()
    {
        Debug.Log("Game Over");
        InitializeAds();
        //Show ads
        Advertisement.Show("Interstitial_Android");
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

    public void OnInitializationComplete()
    {
        //throw new System.NotImplementedException();
        Debug.Log("Unity Ads Initialization Complete.");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        //throw new System.NotImplementedException();
        Debug.LogError($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }
}
