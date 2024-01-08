using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private static CameraShake instance;
    private float shakeIntensity = 0f;
    private float shakeDuration = 0f;
    private Vector3 originalPosition;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        originalPosition = transform.localPosition;
    }

    public static void Shake(float intensity, float duration)
    {
        if (instance != null)
        {
            instance.shakeIntensity = intensity;
            instance.shakeDuration = duration;
        }
        else
        {
            Debug.LogError("CameraShake instance not found. Make sure the script is attached to the camera.");
        }
    }

    private void Update()
    {
        if (shakeDuration > 0)
        {
            transform.localPosition = originalPosition + Random.insideUnitSphere * shakeIntensity;
            shakeDuration -= Time.deltaTime;
        }
        else
        {
            shakeDuration = 0f;
            transform.localPosition = originalPosition;
        }
    }
}
