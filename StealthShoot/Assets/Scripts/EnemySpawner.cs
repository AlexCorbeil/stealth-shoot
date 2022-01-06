using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject bruiserPrefab;
    public GameObject spawnPoint;
    public float spawnTimer;
    public bool isActivated;
    float spawnRate;
    int numOfEnemies;

    private void Awake() {
        References.enemySpawner = this;
    }

    void Start() {
        isActivated = false;
        spawnRate = 0;
        numOfEnemies = 0;
    }

    void FixedUpdate() {
        if (isActivated) {
            spawnRate += Time.deltaTime;

            if (spawnRate >= spawnTimer) {
                SpawnEnemy();
                spawnRate = 0f;
            }
        }
    }

    void SpawnEnemy() {
        if(numOfEnemies == 5) {
            Instantiate(bruiserPrefab, spawnPoint.transform.position, enemyPrefab.transform.rotation);
            numOfEnemies = 0;
            return;
        }

        Instantiate(enemyPrefab, spawnPoint.transform.position + Vector3.right, enemyPrefab.transform.rotation);
        numOfEnemies++;
    }
}
