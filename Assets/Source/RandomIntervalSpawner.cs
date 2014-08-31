using UnityEngine;

public class RandomIntervalSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public float minSpawnInterval;
    public float maxSpawnInterval;
    public int maxWomenSpawn = 3;
    public int spawnDistance = 50;

    public Sprite spriteOpened;
    public AudioClip sfxOpen;
    bool isClosed = true;

    private float currentInterval;
    private float timer;
    private bool dontSpawn;
    private int womenCounter = 0;
    private GameObject player;

    public GameObject guardPrefab;

    private static float CalculateNewInterval(float minInterval, float maxInterval) {
        return Random.Range(minInterval, maxInterval);
    }

    private void Start() {
        currentInterval = CalculateNewInterval(minSpawnInterval, maxSpawnInterval);
        player = GameObject.Find("Player");

        if (Random.Range(0, 100) <= 20)
        {
            Instantiate(guardPrefab, transform.position + new Vector3(0,5,0), Quaternion.identity);
        }
    }

    private void Update() {
        timer += Time.deltaTime;

        if (timer >= currentInterval && womenCounter < maxWomenSpawn)
        {
            timer = 0f;
            currentInterval = CalculateNewInterval(minSpawnInterval, maxSpawnInterval);

            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 3);
            dontSpawn = false;
            foreach(Collider2D c in colliders){
                if(c.gameObject.name == "Player"){
                    dontSpawn = true;
                }
            }

            if (!dontSpawn)
            {                
                if(Mathf.Abs(player.transform.position.x - transform.position.x) < spawnDistance){                 
                    Instantiate(objectToSpawn, transform.position + new Vector3(0f, 1.25f, 0f), transform.rotation);
                    womenCounter++;
                
                    if (isClosed)
                    {
                        Open();
                    }
                }
            }

            

        }
    }

    void Open() {
        GetComponent<SpriteRenderer>().sprite = spriteOpened;
        //audio.PlayOneShot(sfxOpen);
        isClosed = false;
    }
}