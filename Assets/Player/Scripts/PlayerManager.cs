using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public int maxLives = 3;
    private int currentLives;
    public Transform respawnPoint;
    private GameObject currentPlayerInstance;
    public float invulnerabilityTime = 3f;

    void Start()
    {
        currentLives = maxLives;
        Respawn();
    }

    public void TakeDamage()
    {
        Destroy(GameObject.Find("Player(Clone)"));
        currentLives--;

        if (currentLives > 0)
        {
            StartCoroutine(DeathSequence());
        }
        else
        {
            NoLivesLeft();
        }
    }

    IEnumerator DeathSequence()
    {
        yield return new WaitForSeconds(3);
        Respawn();
    }

    private void Respawn()
    {
        StartCoroutine(RespawnSequence());
    }

    IEnumerator RespawnSequence()
    {
        yield return new WaitForSeconds(1); // Placeholder for round start animation
        if (currentPlayerInstance != null)
        {
            Destroy(currentPlayerInstance);
        }
        currentPlayerInstance = Instantiate(playerPrefab, respawnPoint.position, Quaternion.identity);
        StartCoroutine(Invulnerability());
    }

    IEnumerator Invulnerability()
    {
        SpriteRenderer spriteRenderer = currentPlayerInstance.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            float elapsedTime = 0;
            while (elapsedTime < invulnerabilityTime)
            {
                spriteRenderer.enabled = !spriteRenderer.enabled;
                yield return new WaitForSeconds(0.1f);
                elapsedTime += 0.1f;
            }
            spriteRenderer.enabled = true;
        }
    }

    private void NoLivesLeft()
    {
        // Code for game over logic
        Debug.Log("Game over.");
    }
}
