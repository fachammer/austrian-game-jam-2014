using UnityEngine;
using System.Collections;

public class PriestSpawn : MonoBehaviour {

    public float minCooldown = 0.5f;
    public float maxCooldown = 3.0f;

    public float maxDistanceToPlayerToSpawn = 10;
    public GameObject priestPrefab;

    float cooldown = 3f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        TrySpawn();
	}

    void TrySpawn() {
        cooldown -= Time.deltaTime;

        if (cooldown <= 0) {
            Spawn();
            cooldown = Random.Range(minCooldown, maxCooldown);
        }
    }

    void Spawn() {
        GameObject.Instantiate(priestPrefab, transform.position, Quaternion.identity);
    }

    void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, maxDistanceToPlayerToSpawn);
    }
}
