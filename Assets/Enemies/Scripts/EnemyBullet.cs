using UnityEngine;

public class EnemyBullet : MonoBehaviour
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
        if (collider.gameObject.CompareTag("Player"))
        {
            FindAnyObjectByType<PlayerManager>().TakeDamage();
            Destroy(gameObject);
        }
    }
}
