using UnityEngine;
using System.Collections;

public class AlanMovement : MonoBehaviour
{
    public float speed = 5.0f;
    public float waitTimeAtTarget = 1.0f;
    private Vector2 movementDirection;
    private float halfWidth;
    private float halfHeight;
    private Camera mainCamera;
    private bool isEntering;

    void Start()
    {
        movementDirection = new Vector2(1, 1).normalized; // Diagonal movement
        halfWidth = GetComponent<SpriteRenderer>().bounds.extents.x;
        halfHeight = GetComponent<SpriteRenderer>().bounds.extents.y;
        mainCamera = Camera.main;
        isEntering = true;

        EnterPlayField();
    }

    private void EnterPlayField()
    {
        Vector3 targetPosition = GetRandomPositionWithinCamera();
        StartCoroutine(MoveToPosition(targetPosition));
    }

    private Vector3 GetRandomPositionWithinCamera()
    {
        float randomX = Random.Range(mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + halfWidth, mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - halfWidth);
        float fixedY = mainCamera.ViewportToWorldPoint(new Vector3(0, 0.75f, 0)).y;
        return new Vector3(randomX, fixedY, 0);
    }

    private IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 5 * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(waitTimeAtTarget);
        isEntering = false;
    }

    void Update()
    {
        if (!isEntering)
        {
            Move();
            CheckCollisionWithCameraBorders();
        }
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
