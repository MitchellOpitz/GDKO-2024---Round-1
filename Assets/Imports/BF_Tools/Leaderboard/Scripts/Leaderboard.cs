using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dan.Main;

public class Leaderboard : MonoBehaviour
{
    public bool leadboardScene;

    [SerializeField]
    private List<TextMeshProUGUI> highScores;

    private List<int> scoresList = new List<int>(new int[10]);
    private string publicLeaderboardKey =
        "cfc3af2e3c0d4cbc15a8e0a21e0f260486d31fe05f3a6e4994b8ec307fe9ac12";

    private void Start()
    {
        if (leadboardScene)
        {
            GetLeaderboard();
        }
        GetHighScores();
    }

    public void GetLeaderboard()
    {
        LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, ((msg) =>
        {
            for (int i = 0; i < highScores.Count; i++)
            {
                string name = msg[i].Username;
                string score = msg[i].Score.ToString();

                int nameLength = name.Length;
                int scoreLength = score.Length;
                int zerosNeeded = 25 - nameLength - scoreLength;

                string highScoreEntry = (i + 1).ToString() + ".    ";
                if(i == highScores.Count - 1)
                {
                    highScoreEntry = (i + 1).ToString() + ".   ";
                }
                
                highScoreEntry = highScoreEntry + name;
                while (zerosNeeded > 0)
                {
                    highScoreEntry = highScoreEntry + " ";
                    zerosNeeded--;
                }

                highScoreEntry = highScoreEntry + score;

                highScores[i].text = highScoreEntry;
            }
        }));
    }

    public void SetLeaderboardEntry(string username, int score)
    {
        LeaderboardCreator.UploadNewEntry(publicLeaderboardKey, username, score, ((msg) =>
        {
            //GetLeaderboard();
            LeaderboardCreator.ResetPlayer();
        }));
    }

    public void GetHighScores()
    {
        //Debug.Log("Retrieving High Scores.");
        LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, ((msg) =>
        {
            for (int i = 0; i < 10; i++)
            {
                scoresList[i] = (int)msg[i].Score;
                //Debug.Log(scoresList[i]);
            }
        }));
    }

    public bool CheckHighScore(int myScore)
    {
        for (int i = 0; i < scoresList.Count; i++)
        {
            Debug.Log("Comparing: " + myScore + " to " + scoresList[i]);
            if (myScore > scoresList[i])
            {
                Debug.Log("New high score!");
                return true;
            }
        }
        Debug.Log("No High Score.");
        return false;
    }
}
