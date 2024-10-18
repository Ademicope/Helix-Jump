using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dan.Main;

public class Leaderboard : MonoBehaviour
{
    [SerializeField]
    public List<TextMeshProUGUI> names;
    [SerializeField]
    public List<TextMeshProUGUI> scores;

    public string publicLeaderBoardKey =
        "eab22d2f970d4b6f75ce907b6d6be76df821783f46eee5bd83a2ccd032f3d4b7";
    public string secretKey =
        "976c729591a1f43a8dffab517dfc521e7d22198307451428328d38aa2e0e853d76d67c6448dfc8858b4a9c0325ebaad466ea8860a2cc3c34d476ddd5a60c99b1ba122cd1ef948cd937690796a612a4f1804457d839510364bfd51a097d22faf276756d4eeaa01bf79bf8dc1bc196a51c79726e971b29d08f46d30bf52acc3d7b";

    //public GameManager gameManager;

    private void Start()
    {
        GetLeaderboard();
    }
    public void GetLeaderboard()
    {
        LeaderboardCreator.GetLeaderboard(publicLeaderBoardKey, ((msg) =>
        {
            int loopLength = (msg.Length < names.Count ? msg.Length : names.Count);
            for (int i = 0; i < loopLength; i++)
            {
                names[i].text = msg[i].Username;
                scores[i].text = msg[i].Score.ToString();
            }
        }));
    }

    public void SetLeaderboardEntry(string username, int score)
    {
        LeaderboardCreator.UploadNewEntry(publicLeaderBoardKey, username, 
            score, ((msg) => 
            {
                GetLeaderboard();
            }));
    }
}
