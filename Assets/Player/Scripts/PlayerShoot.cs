using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour
{
    public GameObject projectilePrefab;
    public GameObject circlePrefab;
    public GameObject squarePrefab;
    public float projectileSpeed = 10f;
    public Vector2 shootingDirection = Vector2.up;
    public Transform projectileSpawnPoint;
    public float jitterMagnitude = 0.1f;
    public float shootCooldown = 0.5f;

    private float lastShootTime;
    private AudioManager audioManager;

    private void Start()
    {
        audioManager = FindAnyObjectByType<AudioManager>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire2") && !FindAnyObjectByType<PauseGame>().IsPaused() && Time.time - lastShootTime >= shootCooldown)
        {
            Shoot();
            lastShootTime = Time.time;
        }
    }

    private void Shoot()
    {
        if (projectileSpawnPoint == null)
        {
            Debug.LogError("Projectile spawn point not set.");
            return;
        }

        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
        audioManager.PlaySound("PlayerShoot");

        if (projectileRb != null)
        {
            projectileRb.velocity = projectileSpawnPoint.up * projectileSpeed;
        }
        else
        {
            Debug.LogError("Projectile prefab does not have a Rigidbody2D component.");
        }

        StartCoroutine(JitterEffect());
    }

    IEnumerator JitterEffect()
    {
        float originalY = transform.position.y;
        transform.position = new Vector3(transform.position.x, transform.position.y - jitterMagnitude, transform.position.z);
        yield return new WaitForSeconds(0.05f);
        transform.position = new Vector3(transform.position.x, originalY, transform.position.z);
    }

    public void UpdateBullets(string newProjectile)
    {
        switch (newProjectile)
        {
            case "Circle":
                projectilePrefab = circlePrefab;
                break;
            case "Square":
                projectilePrefab = squarePrefab;
                break;
            default:
                Debug.LogWarning("Projectile type not recognized: " + newProjectile);
                break;
        }
    }
}
