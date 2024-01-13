using UnityEngine;
using System.Collections;

public class EnemyShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 5f;
    private GameObject player;

    void Start()
    {
        StartCoroutine(ShootAtPlayerCoroutine());
    }

    private IEnumerator ShootAtPlayerCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2f, 4f));
            ShootAtPlayer();
        }
    }

    void ShootAtPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && bulletPrefab != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            AudioManager.instance.PlaySound("EnemyShoot");
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                Vector2 direction = (player.transform.position - transform.position).normalized;
                rb.velocity = direction * bulletSpeed;
            }
        }
    }
}
