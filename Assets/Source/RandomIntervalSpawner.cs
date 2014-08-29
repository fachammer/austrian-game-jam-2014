using UnityEngine;

public class RandomIntervalSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public float minSpawnInterval;
    public float maxSpawnInterval;

    private float currentInterval;
    private float timer;

    private static float CalculateNewInterval(float minInterval, float maxInterval) {
        return Random.Range(minInterval, maxInterval);
    }

    private void Start() {
        currentInterval = CalculateNewInterval(minSpawnInterval, maxSpawnInterval);
    }

    private void Update() {
        timer += Time.deltaTime;

        if (timer >= currentInterval) {
            timer = 0f;
            currentInterval = CalculateNewInterval(minSpawnInterval, maxSpawnInterval);
            Instantiate(objectToSpawn, transform.position, transform.rotation);
        }
    }
}