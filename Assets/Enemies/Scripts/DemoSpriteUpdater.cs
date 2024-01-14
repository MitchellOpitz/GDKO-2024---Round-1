using UnityEngine;
using UnityEngine.UI;

public class DemoSpriteUpdater : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Image uiImage;

    void Update()
    {
        if (spriteRenderer != null && uiImage != null)
        {
            uiImage.sprite = spriteRenderer.sprite;
        }
    }
}
