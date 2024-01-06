using UnityEngine;

public class BonBonMovement : MonoBehaviour
{
    public float verticalSpeed = 5.0f;
    public float horizontalSpeed = 2.0f;
    public float waveMagnitude = 0.5f;
    private Camera mainCamera;
    private float startX;
    private float startY;
    private float direction = 1.0f;

    void Start()
    {
        mainCamera = Camera.main;
        startX = transform.position.x;
        startY = transform.position.y;
    }

    void Update()
    {
        MoveInWavePattern();
        CheckAndBounceAtCameraBorder();
    }

    void MoveInWavePattern()
    {
        float y = startY + Mathf.Sin(Time.time * verticalSpeed) * waveMagnitude;
        transform.position = new Vector3(transform.position.x + direction * horizontalSpeed * Time.deltaTime, y, transform.position.z);
    }

    void CheckAndBounceAtCameraBorder()
    {
        Vector2 screenPosition = mainCamera.WorldToViewportPoint(transform.position);
        if (screenPosition.x > 1 || screenPosition.x < 0)
        {
            direction *= -1;
        }
    }
}
