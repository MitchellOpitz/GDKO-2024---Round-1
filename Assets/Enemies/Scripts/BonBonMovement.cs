using UnityEngine;
using System.Collections;

public class BonBonMovement : MonoBehaviour
{
    public float verticalSpeed = 5.0f;
    public float horizontalSpeed = 2.0f;
    public float waveMagnitude = 0.5f;
    public float waitTimeAtTarget = 1.0f; // Time to wait at target position
    private Camera mainCamera;
    private float startX;
    private float startY;
    private float direction = 1.0f;
    private bool isEntering;

    void Start()
    {
        mainCamera = Camera.main;
        startX = transform.position.x;
        startY = transform.position.y;
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
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, horizontalSpeed * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(waitTimeAtTarget);
        isEntering = false;
        startY = transform.position.y; // Update startY to current y position after reaching target
    }

    void Update()
    {
        if (!isEntering)
        {
            MoveInWavePattern();
            CheckAndBounceAtCameraBorder();
        }
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
