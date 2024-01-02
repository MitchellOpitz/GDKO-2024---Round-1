using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 1;
    public float maxDistance = 15f;
    private Vector3 launchPosition;

    void Start()
    {
        launchPosition = transform.position;
    }

    void Update()
    {
        if (Vector3.Distance(launchPosition, transform.position) > maxDistance)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = collider.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
