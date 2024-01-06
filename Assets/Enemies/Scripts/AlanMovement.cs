using UnityEngine;

public class AlanMovement : MonoBehaviour
{
    public float speed = 5.0f;
    private Vector2 movementDirection;
    private float halfWidth;
    private float halfHeight;
    private Camera mainCamera;

    void Start()
    {
        movementDirection = new Vector2(1, 1).normalized; // Diagonal movement
        halfWidth = GetComponent<SpriteRenderer>().bounds.extents.x;
        halfHeight = GetComponent<SpriteRenderer>().bounds.extents.y;
        mainCamera = Camera.main;
    }

    void Update()
    {
        Move();
        CheckCollisionWithCameraBorders();
    }

    void Move()
    {
        transform.Translate(movementDirection * speed * Time.deltaTime);
    }

    void CheckCollisionWithCameraBorders()
    {
        Vector3 positionWithOffset = transform.position + new Vector3(halfWidth * Mathf.Sign(movementDirection.x), halfHeight * Mathf.Sign(movementDirection.y), 0);
        Vector2 screenPosition = mainCamera.WorldToViewportPoint(positionWithOffset);
        bool bounced = false;

        if (screenPosition.x > 1 || screenPosition.x < 0)
        {
            movementDirection.x = -movementDirection.x;
            bounced = true;
        }
        if (screenPosition.y > 1 || screenPosition.y < 0)
        {
            movementDirection.y = -movementDirection.y;
            bounced = true;
        }

        if (bounced)
        {
            float clampedX = Mathf.Clamp(screenPosition.x, 0, 1);
            float clampedY = Mathf.Clamp(screenPosition.y, 0, 1);

            Vector3 adjustedPosition = mainCamera.ViewportToWorldPoint(new Vector3(clampedX, clampedY, mainCamera.nearClipPlane));

            float adjustedX = adjustedPosition.x - halfWidth * Mathf.Sign(movementDirection.x);
            float adjustedY = adjustedPosition.y - halfHeight * Mathf.Sign(movementDirection.y);

            transform.position = new Vector3(adjustedX, adjustedY, 0);
        }
    }
}
