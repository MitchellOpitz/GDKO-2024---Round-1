using UnityEngine;

public class Powerup : MonoBehaviour
{
    public Sprite type1Sprite;
    public Sprite type2Sprite;
    public Sprite type3Sprite;
    private int powerupType;

    void Start()
    {
        powerupType = Random.Range(1, 4); // Randomly pick a number between 1 and 3
        SetSprite();
    }

    void SetSprite()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        switch (powerupType)
        {
            case 1:
                spriteRenderer.sprite = type1Sprite;
                break;
            case 2:
                spriteRenderer.sprite = type2Sprite;
                break;
            case 3:
                spriteRenderer.sprite = type3Sprite;
                break;
        }
    }

    public void ActivateEffect()
    {
        switch (powerupType)
        {
            case 1:
                // Trigger effect for PowerUp type 1
                break;
            case 2:
                // Trigger effect for PowerUp type 2
                break;
            case 3:
                // Trigger effect for PowerUp type 3
                break;
        }
    }

    // Additional logic for how the powerup is collected or activated can be added here
}
