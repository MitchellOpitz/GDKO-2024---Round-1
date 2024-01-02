using UnityEngine;

public class Powerup : MonoBehaviour
{
    public Sprite type1Sprite;
    public Sprite type2Sprite;
    public Sprite type3Sprite;
    public float speed = 1f;
    public float offScreenOffset = 1f;

    private int powerupType;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        powerupType = Random.Range(1, 4);
        SetSprite();
    }

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        if (IsOffScreen())
        {
            Destroy(gameObject);
        }
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

    private bool IsOffScreen()
    {
        Vector2 positionOnScreen = mainCamera.WorldToViewportPoint(transform.position);
        return positionOnScreen.y < -offScreenOffset;
    }

    public void ActivateEffect()
    {
        switch (powerupType)
        {
            case 1:
                Debug.Log("Effect for PowerUp type 1 Activated.");
                break;
            case 2:
                Debug.Log("Effect for PowerUp type 2 Activated.");
                break;
            case 3:
                Debug.Log("Effect for PowerUp type 3 Activated.");
                break;
        }

        Destroy(gameObject);
    }
}
