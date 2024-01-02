using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f;
    public Sprite leftSprite;
    public Sprite rightSprite;
    public Sprite stoppedSprite;
    private bool isMovingRight = false;
    private float halfPlayerWidth;
    private SpriteRenderer spriteRenderer;
    private Animator thrusterAnimator;

    void Start()
    {
        Initialize();
    }

    void Update()
    {
        HandleInput();
        MovePlayer();
        UpdateSpriteAndAnimation();
    }

    void Initialize()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        halfPlayerWidth = spriteRenderer.bounds.extents.x;
        thrusterAnimator = transform.Find("Thrusters").GetComponent<Animator>();
    }

    void HandleInput()
    {
        if (Input.GetButtonDown("Fire1"))
            isMovingRight = true;
        else if (Input.GetButtonUp("Fire1"))
            isMovingRight = false;
    }

    void MovePlayer()
    {
        float movementDirection = isMovingRight ? 1 : -1;
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x + movementDirection * speed * Time.deltaTime,
            -GetScreenWidth() + halfPlayerWidth, GetScreenWidth() - halfPlayerWidth),
            transform.position.y,
            transform.position.z
        );
    }

    void UpdateSpriteAndAnimation()
    {
        int animationState = 0; // 0 for stopped, -1 for left, 1 for right

        if (transform.position.x <= -GetScreenWidth() + halfPlayerWidth || transform.position.x >= GetScreenWidth() - halfPlayerWidth)
        {
            spriteRenderer.sprite = stoppedSprite;
            animationState = 0;
        }
        else
        {
            spriteRenderer.sprite = isMovingRight ? rightSprite : leftSprite;
            animationState = isMovingRight ? 1 : -1;
        }

        thrusterAnimator.SetInteger("MovementDirection", animationState);
    }

    float GetScreenWidth()
    {
        return Camera.main.orthographicSize * Camera.main.aspect;
    }
}
