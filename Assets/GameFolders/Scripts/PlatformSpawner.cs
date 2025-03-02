using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    private const int POOL_SIZE = 15;

    public GameObject platformPrefab;
    public float spawnInterval = 2f;
    public float platformGap = 2.5f;

    private Queue<GameObject> platformPool;
    private List<GameObject> activePlatforms;

    private float nextSpawnY;
    private float minX,maxX;

    void Start()
    {
        startCamSet();

        platformPool = new Queue<GameObject>();
        activePlatforms = new List<GameObject>();

        for (int i = 0; i < POOL_SIZE; i++)
        {
            GeneratePlatform(i == 0 ? 0f : nextSpawnY);
        }

    }
    private void Update()
    {
        SpawnPlatform();
        CheckAndDisablePlatforms();
    }
    private void GeneratePlatform(float yPos)
    {
        float randomX = Random.Range(minX, maxX);
        Vector3 spawnPosition = new Vector3(randomX, yPos, 0);
        GameObject platform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
        platform.SetActive(false);
        platformPool.Enqueue(platform);
        nextSpawnY += platformGap;
    }
    private GameObject GetPlatformFromPool()
    {
        if (platformPool.Count > 0)
        {
            GameObject platform = platformPool.Dequeue();
            activePlatforms.Add(platform);
            platform.SetActive(true);
            return platform;
        }
        return null;
    }

    private void ReturnPlatformToPool(GameObject platform)
    {
        platform.SetActive(false);
        activePlatforms.Remove(platform);
        platformPool.Enqueue(platform);
    }

    private void SpawnPlatform()
    {
        if (Camera.main.transform.position.y > 11f)
        {
            GameObject platform = GetPlatformFromPool();
            if (platform != null)
            {
                float randomX = Random.Range(minX, maxX);
                platform.transform.position = new Vector3(randomX, nextSpawnY, 0);
                platform.SetActive(true);
                nextSpawnY += platformGap;

            }
        }
    }
    private void startCamSet()
    {
        float screenWidth = Camera.main.aspect * Camera.main.orthographicSize * 2f;
        minX = -screenWidth + 1f;
        maxX = screenWidth - 1f;
        nextSpawnY = -17.5f;
    }
    private void CheckAndDisablePlatforms()
    {
        for (int i = activePlatforms.Count - 1; i >= 0; i--)
        {
            GameObject platform = activePlatforms[i];

            if (platform.transform.position.y + 10f < Camera.main.transform.position.y)
            {
                ReturnPlatformToPool(platform);
            }
        }
    }
}

