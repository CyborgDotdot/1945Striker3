using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnManager : MonoBehaviour
{
    public bool enableSpawn = false;

    public GameObject monsterPrefab; 
    public float spawnInterval = 1.0f;
    public int maxMonsters = 6;

    private Camera mainCamera;

    void SpawnMonster()
    {
        int monsterCount = GameObject.FindGameObjectsWithTag("Monster").Length;

        if (enableSpawn && monsterCount < maxMonsters)
        {
            for (int i = 0; i < 3; i++)
            {
                Vector3 spawnPosition = GetRandomSpawnPosition();
                Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);
            }
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        Vector3 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector3 topRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));

        float randomX = Random.Range(bottomLeft.x, topRight.x);

        Vector3 spawnPosition = new Vector3(randomX, transform.position.y, 0f);
        return spawnPosition;
    }

    void Start()
    {
        mainCamera = Camera.main;
        InvokeRepeating("SpawnMonster", 1.0f, spawnInterval);
    }
}
