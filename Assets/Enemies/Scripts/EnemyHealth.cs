using UnityEngine;
using TMPro;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int score = 100;
    public GameObject powerUpPrefab; // Placeholder for power-up prefab
    public float spawnChance = 0.1f; // 10% chance to spawn a power-up
    public GameObject particlePrefab;

    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        ScoreManager.Instance.AddScore(score);
        //TrySpawnPowerUp();
        FindObjectOfType<EnemyManager>().EnemyDefeated(gameObject);
        Instantiate(particlePrefab, transform.position, Quaternion.identity);
        CameraShake.Shake(0.1f, 0.2f);
        FindAnyObjectByType<ScorePopup>().ShowScorePopup(score, transform.position);
        Destroy(gameObject);
    }

    void TrySpawnPowerUp()
    {
        if (Random.value < spawnChance)
        {
            Instantiate(powerUpPrefab, transform.position, Quaternion.identity);
        }
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }

}
