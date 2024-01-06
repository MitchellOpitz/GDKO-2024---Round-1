using UnityEngine;

public class LipsMovement : MonoBehaviour
{
    public float diagonalSpeed = 3.0f;
    public float verticalRiseSpeed = 5.0f;
    public float lowerThreshold = -3.0f;
    public float upperThreshold = 3.0f;
    private Vector2 movementDirection;
    private Camera mainCamera;
    private bool rising = false;

    void Start()
    {
        movementDirection = new Vector2(1, -1).normalized; // Initial diagonal movement
        mainCamera = Camera.main;
    }

    void Update()
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
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }
    }
    void OnDrawGizmos()
    {
        // Set the color for the lower threshold line
        Gizmos.color = Color.red;
        // Draw a horizontal line for the lower threshold
        Gizmos.DrawLine(new Vector3(-10, lowerThreshold, 0), new Vector3(10, lowerThreshold, 0));

        // Set the color for the upper threshold line
        Gizmos.color = Color.green;
        // Draw a horizontal line for the upper threshold
        Gizmos.DrawLine(new Vector3(-10, upperThreshold, 0), new Vector3(10, upperThreshold, 0));
    }
}
