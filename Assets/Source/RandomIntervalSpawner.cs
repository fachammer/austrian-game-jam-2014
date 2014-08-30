using UnityEngine;

public class RandomIntervalSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public float minSpawnInterval;
    public float maxSpawnInterval;

    public Sprite spriteOpened;
    public AudioClip sfxOpen;
    bool isClosed = true;

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
            Instantiate(objectToSpawn, transform.position + new Vector3(0f, 1.25f, 0f), transform.rotation);

            if (isClosed) {
                Open();
            }

        }
    }

    void Open() {
        GetComponent<SpriteRenderer>().sprite = spriteOpened;
        //audio.PlayOneShot(sfxOpen);
        isClosed = false;
    }
}