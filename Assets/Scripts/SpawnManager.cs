using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject spawnPrefab;
    public GameObject powerupPrefab;
    public int enemyCount = 0;
    public int waveNumber = 1;

    private float spawnPositionLimit = 9;
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(waveNumber);
        Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;

        if(enemyCount == 0)
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber);
            Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
        }
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(spawnPrefab, GenerateSpawnPosition(), spawnPrefab.transform.rotation);
        }
    }

    Vector3 GenerateSpawnPosition()
    {
        float spawnPositionX = Random.Range(-spawnPositionLimit, spawnPositionLimit);
        float spawnPositionZ = Random.Range(-spawnPositionLimit, spawnPositionLimit);
        return new Vector3(spawnPositionX, 1, spawnPositionZ);
    }
}
