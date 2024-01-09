using UnityEngine;
using UnityEngine.UI;

public class LivesRemaining : MonoBehaviour
{
    public Sprite[] numberSprites; // Assign this in the inspector, with 0-9 sprites
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer not found on the object.");
        }
    }

    public void UpdateLivesDisplay(int livesRemaining)
    {
        if (livesRemaining >= 0 && livesRemaining < numberSprites.Length)
        {
            spriteRenderer.sprite = numberSprites[livesRemaining];
        }
        else
        {
            Debug.LogError("Invalid number of lives remaining.");
        }
    }
}
