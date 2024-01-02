using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileSpeed = 10f;
    public Vector2 shootingDirection = Vector2.up;

    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();

        if (projectileRb != null)
        {
            projectileRb.velocity = shootingDirection.normalized * projectileSpeed;
        }
        else
        {
            Debug.LogError("Projectile prefab does not have a Rigidbody2D component.");
        }
    }
}
