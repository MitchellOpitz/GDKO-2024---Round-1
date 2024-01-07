using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    public UnityEvent<string, int> submitScoreEvent;

    public GameObject highScoreUI;


    public int Score { get; private set; }
    public TextMeshProUGUI scoreText;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int amount)
    {
        Score += amount;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        scoreText.text = "SCORE: " + Score.ToString("D7");
    }

    public void CheckHighScore()
    {
        Leaderboard leaderboard = FindAnyObjectByType<Leaderboard>();
        if (leaderboard.CheckHighScore(Score))
        {
            Debug.Log("New high score!");
            highScoreUI.SetActive(true);
            TextMeshProUGUI highScoreText = GameObject.Find("HighScoreText").GetComponent<TextMeshProUGUI>();
            highScoreText.text = Score.ToString();

        }
    }

    public void SubmitScore(TMP_InputField inputName)
    {
        submitScoreEvent.Invoke(inputName.text, Score);
        SceneLoader.Instance.LoadSceneByName("HighScores");
    }
}
