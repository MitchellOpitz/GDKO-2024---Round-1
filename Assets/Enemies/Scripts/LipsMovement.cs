using UnityEngine;
using System.Collections;

public class LipsMovement : MonoBehaviour
{
    public float diagonalSpeed = 3.0f;
    public float verticalRiseSpeed = 5.0f;
    public float lowerThreshold = -3.0f;
    public float upperThreshold = 3.0f;
    public float waitTimeAtTarget = 1.0f; // Time to wait at target position
    private Vector2 movementDirection;
    private Camera mainCamera;
    private bool rising = false;
    private bool isEntering;

    void Start()
    {
        movementDirection = new Vector2(1, -1).normalized; // Initial diagonal movement
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
        float randomX = Random.Range(mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x, mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x);
        float fixedY = mainCamera.ViewportToWorldPoint(new Vector3(0, 0.75f, 0)).y;
        return new Vector3(randomX, fixedY, 0);
    }

    private IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, diagonalSpeed * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(waitTimeAtTarget);
        isEntering = false;
    }

    void Update()
    {
        if (!isEntering)
        {
            if (!rising)
            {
                MoveDiagonally();
                if (transform.position.y <= lowerThreshold)
                {
                    rising = true;
                }
            }
            else
            {
                MoveUp();
                if (transform.position.y >= upperThreshold)
                {
                    rising = false;
                }
            }

            CheckAndReflectAtScreenEdges();
        }
    }

    void MoveDiagonally()
    {
        transform.Translate(movementDirection * diagonalSpeed * Time.deltaTime);
    }

    void MoveUp()
    {
        transform.Translate(Vector2.up * verticalRiseSpeed * Time.deltaTime);
    }

    void CheckAndReflectAtScreenEdges()
    {
        Vector2 screenPosition = mainCamera.WorldToViewportPoint(transform.position);
        if (screenPosition.x > 1 || screenPosition.x < 0)
        {
            movementDirection.x *= -1; // Reflect horizontally
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(-10, lowerThreshold, 0), new Vector3(10, lowerThreshold, 0));

        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector3(-10, upperThreshold, 0), new Vector3(10, upperThreshold, 0));
    }
}
