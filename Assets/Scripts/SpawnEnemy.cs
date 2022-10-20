using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnEnemy : MonoBehaviour
{
    public int EnemyCount;

    public int BossEnemyCount;

    public GameObject EnemyPrefab;
    public GameObject BossEnemyPrefab;

    private float NextEnemySpawnTime = 5;
    private float NewNextEnemySpawnTime = 5;
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemys();
    }

    void SpawnEnemys()
    {
        for (int i = 0; i < EnemyCount; i++)
        {
            GameObject enemy = EnemyPrefab.Spawn(new Vector3(Random.Range(-100, 100), 1f, Random.Range(-100, 100)));
            enemy.transform.SetParent(this.gameObject.transform);
        }

        GameObject enemyBoss = BossEnemyPrefab.Spawn(new Vector3(Random.Range(-100, 100), 2f, Random.Range(-100, 100)));
        enemyBoss.transform.SetParent(this.gameObject.transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (NextEnemySpawnTime <= 0 && NewNextEnemySpawnTime>2)
        {
            NextEnemySpawnTime = NewNextEnemySpawnTime-1;
            SpawnEnemys();
        }
        else if(NewNextEnemySpawnTime==2)
        {
            NextEnemySpawnTime = NewNextEnemySpawnTime;
            SpawnEnemys();
        }
    }

    private void FixedUpdate()
    {
        NextEnemySpawnTime -= Time.deltaTime;
    }
}
