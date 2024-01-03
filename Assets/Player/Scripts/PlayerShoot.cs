using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject projectilePrefab;
    public GameObject circlePrefab;
    public GameObject squarePrefab;
    public float projectileSpeed = 10f;
    public Vector2 shootingDirection = Vector2.up;
    public Transform projectileSpawnPoint;

    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            Shoot();
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

        if (projectileRb != null)
        {
            projectileRb.velocity = projectileSpawnPoint.up * projectileSpeed;
        }
        else
        {
            Debug.LogError("Projectile prefab does not have a Rigidbody2D component.");
        }
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
