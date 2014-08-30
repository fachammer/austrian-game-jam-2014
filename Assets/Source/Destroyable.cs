using UnityEngine;
using System.Collections;

public class Destroyable : MonoBehaviour {
    PlayerController player;
    CircleCollider2D playerColl;
    Sprite spriteDestroyed;
    AudioClip sfxDestroy;

    bool isDestroyed = false;

    void Awake() {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        playerColl = player.GetComponent<CircleCollider2D>();
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (!isDestroyed) {
            if (player.isRolling) {
                float sqrDist = (player.transform.position - transform.position).magnitude;

                if (sqrDist <= playerColl.radius * 1.4) {
                    isDestroyed = true;
                    GetComponent<SpriteRenderer>().sprite = spriteDestroyed;
                    collider2D.enabled = false;
                    audio.clip = sfxDestroy;
                    audio.Play();
                }
            }
        }
	}

}
