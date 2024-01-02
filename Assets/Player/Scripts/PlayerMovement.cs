using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f;
    private bool isMovingRight = false;
    private float halfPlayerWidth;

    void Start()
    {
        // Calculate half of the player's width
        halfPlayerWidth = GetComponent<SpriteRenderer>().bounds.extents.x;
    }

    void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        if (Input.GetButtonDown("Fire1"))  // Replace "Fire1" with your button
        {
            isMovingRight = true;
        }

        if (Input.GetButtonUp("Fire1"))  // Replace "Fire1" with your button
        {
            isMovingRight = false;
        }

        Vector3 position = transform.position;

        if (isMovingRight)
        {
            position.x += speed * Time.deltaTime;
        }
        else
        {
            position.x -= speed * Time.deltaTime;
        }

        // Clamp position
        position.x = Mathf.Clamp(position.x, -GetScreenWidth() + halfPlayerWidth, GetScreenWidth() - halfPlayerWidth);

        transform.position = position;
    }

    float GetScreenWidth()
    {
        return Camera.main.orthographicSize * Camera.main.aspect;
    }
}
