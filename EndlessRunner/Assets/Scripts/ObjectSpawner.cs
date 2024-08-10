using System.Collections;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] GameObject coinPrefab;
    [SerializeField] GameObject firePrefab; 
    [SerializeField] GameObject roadPrefab; 
    [SerializeField] float initialSpawnInterval = 0.5f;
    [SerializeField] float initialRoadSpawnInterval = 0.5f;
    [SerializeField] float minimumSpawnInterval = 0.05f; 
    [SerializeField] float spawnIntervalDecreaseRate = 0.01f; 
    [SerializeField] float objectMoveSpeed = 10.0f; 
    [SerializeField] float speedIncreaseRate = 0.2f;
    [SerializeField] Vector3 roadOffset;
    [SerializeField] float laneDistance = 2.0f; 

    private float spawnTimer, roadSpawnTimer;
    private float currentSpawnInterval, currentRoadSpawnTimer;

    void Start()
    {
        currentSpawnInterval = initialSpawnInterval;
        spawnTimer = currentSpawnInterval;

        currentRoadSpawnTimer = initialRoadSpawnInterval;
        roadSpawnTimer = currentRoadSpawnTimer;
        Instantiate(roadPrefab, roadOffset, Quaternion.identity);
    }

    void Update()
    {
        spawnTimer -= Time.deltaTime;
        roadSpawnTimer -= Time.deltaTime;

        if(roadSpawnTimer <= 0)
        {
            Instantiate(roadPrefab,  roadOffset , Quaternion.identity);
            roadSpawnTimer = currentRoadSpawnTimer;
        }
        if (spawnTimer <= 0)
        {
            SpawnObject();
            spawnTimer = currentSpawnInterval;

            // Decrease spawn interval over time
            if (currentSpawnInterval > minimumSpawnInterval)
            {
                currentSpawnInterval -= spawnIntervalDecreaseRate * Time.deltaTime;
            }

            // Increase object speed over time
            objectMoveSpeed += speedIncreaseRate;
        }
    }
    void SpawnObject()
    {
        int randomObject = Random.Range(0, 2); // Randomly select between coin and fire (0 = coin, 1 = fire)
        GameObject spawnedObject = null;

        // Choose a random lane: 0 (left), 1 (middle), or 2 (right)
        int randomLane = Random.Range(0, 3);
        Vector3 spawnPosition = transform.position + Vector3.right * (randomLane - 1) * laneDistance * 2;
        if (randomObject == 0)
        {
            spawnedObject = Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            spawnedObject = Instantiate(firePrefab, spawnPosition, Quaternion.identity);
        }

        spawnedObject.GetComponent<ObjectMover>().moveSpeed = objectMoveSpeed;
    }
}
