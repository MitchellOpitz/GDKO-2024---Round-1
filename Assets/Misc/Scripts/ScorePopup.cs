using UnityEngine;
using System.Collections;
using TMPro;

public class ScorePopup : MonoBehaviour
{
    public TextMeshProUGUI scoreTextPrefab;
    public Canvas canvas;

    public void ShowScorePopup(int score, Vector3 position)
    {
        TextMeshProUGUI scoreText = Instantiate(scoreTextPrefab, canvas.transform);
        RectTransform rectTransform = scoreText.GetComponent<RectTransform>();
        rectTransform.position = Camera.main.WorldToScreenPoint(position);
        scoreText.text = $"+{score}";
        StartCoroutine(MoveAndFade(scoreText));
    }

    private IEnumerator MoveAndFade(TextMeshProUGUI scoreText)
    {
        float duration = 1f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            scoreText.rectTransform.anchoredPosition += Vector2.up * Time.deltaTime * 50f;
            Color textColor = scoreText.color;
            textColor.a = Mathf.Lerp(1f, 0f, elapsedTime / duration);
            scoreText.color = textColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(scoreText.gameObject);
    }
}
