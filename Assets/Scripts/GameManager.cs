using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
//using UnityEditor.Advertisements;
using UnityEngine;
using Debug = UnityEngine.Debug;
using UnityEngine.Advertisements;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public int bestScore;
    public int score;

    public int currentStage = 0;

    public static GameManager singleton;

    public GameObject leaderboardScreen;
    public Button continueButton;
    //public TMP_InputField playerUsername;

    private HelixController helixController;
    //private Leaderboard leaderboard;
    private const string gameId = "5714049";
    private const string placementId = "Interstitial_Android";

    // Start is called before the first frame update
    void Awake()
    {
        helixController = FindObjectOfType<HelixController>();
        //leaderboard = GetComponent<Leaderboard>();

        if (singleton == null)
        {
            singleton = this;
            InitializeAds();
        }
        else if (singleton == this)
        {
            Destroy(gameObject);
        }

        bestScore = PlayerPrefs.GetInt("Highscore");
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
            LoadAd();
        }
    }

    public void LoadAd()
    {
        // IMPORTANT! Only load content AFTER initialization
        Debug.Log("Loading Ad: " + gameId);
        Advertisement.Load(placementId, this);
    }

    // Show the loaded content in the Ad Unit:
    public void ShowAd()
    {
        Debug.Log("Showing Ad: " + gameId);

        Time.timeScale = 0;
        Advertisement.Show(placementId, this);
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
        
        //InitializeAds();

        //Show ads
        ShowAd();
        
        FindObjectOfType<BallController>().ResetBall();
        singleton.score = 0;
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
        LoadAd();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        //throw new System.NotImplementedException();
        Debug.LogError($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        //throw new System.NotImplementedException();
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        //throw new System.NotImplementedException();
        Debug.LogError($"Error loading Ad Unit: {placementId} - {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        //throw new System.NotImplementedException();
        Debug.LogError($"Error showing Ad Unit {placementId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        //throw new System.NotImplementedException();
        Debug.Log("Ad started showing.");
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        //throw new System.NotImplementedException();
        Debug.Log("Ad clicked.");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        //throw new System.NotImplementedException();
        Debug.Log("Ad completed.");
        leaderboardScreen.gameObject.SetActive(true);

        StartCoroutine(WaitForLeaderboardToClose());
    }

    IEnumerator WaitForLeaderboardToClose()
    {
        // Assuming the leaderboard is closed when the screen is set inactive
        yield return new WaitUntil(() => !leaderboardScreen.activeSelf);

        ContinueGame();
    }

    public void ContinueGame()
    {
        leaderboardScreen.gameObject.SetActive(false);
        // Resume the game once the leaderboard is closed
        Time.timeScale = 1;
    }
}
