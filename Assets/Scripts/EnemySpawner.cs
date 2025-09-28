using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public float spawnInterval = 5;
    public float spawnIntervalIncrement = 0.5f;
    public float difficultyInterval = 10;
    public float maxLifeTime = 4f;

    private float nextSpawn = 0;
    private float nextDifficulty = 0;

    void Start()
    {

    }

    void Update()
    {
        if (Time.time > nextDifficulty && spawnInterval > 1)
        {
            nextDifficulty = Time.time + difficultyInterval;
            spawnInterval = spawnInterval - spawnIntervalIncrement;
        }

        if (Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnInterval;

            // Instanciar un meteorito y ponerle contador de muerte
            float rand = Random.Range(-7.5f, 7.5f);
            Vector2 spawnPosition = new Vector2(rand, 7.5f);
            GameObject asteroid = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);
            Destroy(asteroid, maxLifeTime);
        }
    }
}
