using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] Transform spawnPos;
    [SerializeField] float spawnDuration;
    float spawnTimer;
    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = spawnDuration;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.enemies.Count <= 1)
        {
            if(spawnTimer <= 0)
            {
                Spawn();
                Debug.Log("Spawned");
                spawnTimer = spawnDuration;
            }
            else
            {
                spawnTimer -= Time.deltaTime;
            }
        }
    }

    void Spawn()
    {
        Instantiate(enemy, spawnPos.position, Quaternion.identity);
    }
}
