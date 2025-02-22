using Assets.GameFolders.Interfaces;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour , IGameState
{
    public GameObject platformPrefab;
    public float spawnInterval = 2f;
    public float startY = 0f;
    public float platformGap = 2.5f;

    private float nextSpawnY;
    private float minX,maxX;
    private bool isJumping = false;
    private bool gameOver = false;

    public bool IsGameOver { get => gameOver; set => gameOver = false; }

    void Start()
    {
        float screenWidth = Camera.main.orthographicSize * Screen.width / Screen.height;
        minX = -screenWidth + 1f;
        maxX = screenWidth - 1f;

        nextSpawnY = startY;
        SpawnPlatform();
        InvokeRepeating("SpawnPlatform", spawnInterval, spawnInterval);
    }

    private void Update()
    {
        if (isJumping) 
        {
            SpawnPlatform();
        }
        if (gameOver) {
            CancelInvoke("SpawnPlatform");
        }
    


    }

    void SpawnPlatform()
    {
        if (gameOver)
        {
            Debug.Log("Game OVER!");
        }
        if (nextSpawnY > Camera.main.transform.position.y - 10f)
        {
            float randomX = Random.Range(minX, maxX);
            Vector3 spawnPosition = new Vector3(randomX, nextSpawnY, 0);
            Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
            nextSpawnY += platformGap;
        }
    }
    public void StartJumping() 
    {
        isJumping = true;
    }
    public void StopJumping()
    {
        isJumping = false;
    }
}

