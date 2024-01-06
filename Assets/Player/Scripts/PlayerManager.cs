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
        yield return new WaitForSeconds(1);
        if (currentPlayerInstance != null)
        {
            Destroy(currentPlayerInstance);
        }
        currentPlayerInstance = Instantiate(playerPrefab, respawnPoint.position, Quaternion.identity);
        StartCoroutine(Invulnerability());
    }

    IEnumerator Invulnerability()
    {
        SpriteRenderer[] spriteRenderers = currentPlayerInstance.GetComponentsInChildren<SpriteRenderer>();
        float elapsedTime = 0;
        while (elapsedTime < invulnerabilityTime)
        {
            foreach (var renderer in spriteRenderers)
            {
                renderer.enabled = !renderer.enabled;
            }
            yield return new WaitForSeconds(0.1f);
            elapsedTime += 0.1f;
        }
        foreach (var renderer in spriteRenderers)
        {
            renderer.enabled = true;
        }
    }

    private void NoLivesLeft()
    {
        Debug.Log("Game over, darling.");
    }
}
