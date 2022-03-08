using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject applePrefab;
    public GameObject bombPrefab;
    public BoxCollider2D spawnArea;
    private float bombSpawnDuration;

    public static GameManager instance;


    private void Start()
    {
        instance = this;
        SpawnAppleToRandomPosition();
    }
    private void OnEnable()
    {
        InvokeRepeating("SpawnBombToRandomPosition", Random.Range(20, 40), Random.Range(20, 40));
    }

    public void SpawnAppleToRandomPosition()
    {
        Bounds bounds = spawnArea.bounds;
        float xPosition = Random.Range(bounds.min.x, bounds.max.x);
        float yPosition = Random.Range(bounds.min.y, bounds.max.y);

        Instantiate(applePrefab, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
    }
    public void SpawnBombToRandomPosition()
    {
        Bounds bounds = spawnArea.bounds;
        float xPosition = Random.Range(bounds.min.x, bounds.max.x);
        float yPosition = Random.Range(bounds.min.y, bounds.max.y);
        Instantiate(bombPrefab, new Vector3(xPosition, yPosition, 0), Quaternion.identity);

    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }


}
