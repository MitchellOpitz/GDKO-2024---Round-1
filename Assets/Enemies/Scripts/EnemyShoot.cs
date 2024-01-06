using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 5f;
    public float shootingInterval = 3f;
    private GameObject player;

    void Start()
    {
        InvokeRepeating("ShootAtPlayer", shootingInterval, shootingInterval);
    }

    void ShootAtPlayer()
    {
        Debug.Log("Shooting now.");
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && bulletPrefab != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                Vector2 direction = (player.transform.position - transform.position).normalized;
                rb.velocity = direction * bulletSpeed;
            }
        }
    }
}
