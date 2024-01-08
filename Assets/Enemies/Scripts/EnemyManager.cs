using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Array to hold different enemy prefabs
    private Dictionary<GameObject, int> activeEnemies = new Dictionary<GameObject, int>(); // Track active enemies

    void Start()
    {
        foreach (var enemyPrefab in enemyPrefabs)
        {
            SpawnEnemy(enemyPrefab);
        }
    }

    public void SpawnEnemy(GameObject enemyPrefab)
    {
        Vector3 spawnPosition = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0f, 1f), 1.1f, 10));
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        // Check for a parent GameObject called "Enemies"
        GameObject enemiesParent = GameObject.Find("Enemies");
        if (enemiesParent == null)
        {
            enemiesParent = new GameObject("Enemies");
        }
        newEnemy.transform.parent = enemiesParent.transform;

        activeEnemies.Add(newEnemy, enemyPrefab.GetInstanceID());

        // Add a script or method call to move the enemy into the playfield
    }


    public void EnemyDefeated(GameObject defeatedEnemy)
    {
        int prefabID = activeEnemies[defeatedEnemy];
        activeEnemies.Remove(defeatedEnemy);

        foreach (var enemyPrefab in enemyPrefabs)
        {
            if (enemyPrefab.GetInstanceID() == prefabID)
            {
                SpawnEnemy(enemyPrefab);
                break;
            }
        }
    }
}
