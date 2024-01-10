using UnityEngine;
using UnityEngine.UI;

public class LivesRemaining : MonoBehaviour
{
    public Sprite[] numberSprites; // Assign this in the inspector, with 0-9 sprites
    public Image sourceImage;

    void Start()
    {
        if (sourceImage == null)
        {
            Debug.LogError("SpriteRenderer not found on the object.");
        }
    }

    public void UpdateLivesDisplay(int livesRemaining)
    {
        if (livesRemaining >= 0 && livesRemaining < numberSprites.Length)
        {
            sourceImage.sprite = numberSprites[livesRemaining];
        }
        else
        {
            Debug.LogError("Invalid number of lives remaining.");
        }
    }

}
