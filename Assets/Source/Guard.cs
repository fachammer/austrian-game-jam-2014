using UnityEngine;
using System.Collections;

public class Guard : MonoBehaviour {

    public float moveForce;
    public float jumpStrength;
    public float jumpCooldown;
    public float evasionClearDistance;
    public float jumpCastLength = 1f;
    public bool doRandomJumps = true;
    public float attackCooldown = 2f;

    float timeTillAttack = 0f;
    int moveDir = 0;
    bool jump = false;
    bool evasionJump = false;
    float timeTillRndJump;
    float timeSinceJump = 0;

    float spawnX;
    public float maxDistToSpawn = 4;

    public AudioClip sfxHit, sfxDeath;
    

    PlayerController player;
    CircleCollider2D playerColl;
    CircleCollider2D coll;

    void Awake() {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        playerColl = player.GetComponent<CircleCollider2D>();
        coll = GetComponent<CircleCollider2D>();

        Collider2D[] playerColls = player.GetComponents<Collider2D>();
        foreach (Collider2D c in playerColls) {
            Physics2D.IgnoreCollision(collider2D, c);
        }

        GetComponent<Health>().OnDeath += new Health.HealthHandler(Guard_OnDeath);
        GetComponent<Health>().OnHit += new Health.HealthHandler(Guard_OnHit);
    }



    void Guard_OnHit() {
        audio.clip = sfxHit;
        audio.Play();
    }

    void Guard_OnDeath() {
        audio.clip = sfxDeath;
        audio.Play();
        
            renderer.enabled = false;
        collider2D.enabled = false;
        Invoke("Deactivate", 2);
    }


    void Deactivate() {
        Destroy(gameObject);
    }

	// Use this for initialization
	void Start () {
        timeTillRndJump = Random.Range(0.5f, 3.0f);
        spawnX = transform.position.x;
        moveDir = 1;
	}
	
	// Update is called once per frame
	void Update () {
        Move();

        CheckPlayerColl();
        timeSinceJump += Time.deltaTime;
        HandleObstacles();
        DoEvasionStuff();
	}

    void CheckPlayerColl() {
        if (timeTillAttack <= 0) {
            float dist = (transform.position - player.transform.position).magnitude;
            if (dist <= playerColl.radius + coll.radius) {

                player.GetComponent<Health>().TakeDamage(1);
                timeTillAttack = attackCooldown;
            }
        }
        else {
            timeTillAttack -= Time.deltaTime;
        }
    }

    void Move() {
        // move from left to right 
        float distToSpawn = spawnX - transform.position.x;
        if (distToSpawn > maxDistToSpawn) {
            moveDir = 1;
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * moveDir, transform.localScale.y, transform.localScale.z);
        }
        else if (distToSpawn < -maxDistToSpawn) {
            moveDir = -1;
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * moveDir, transform.localScale.y, transform.localScale.z);
        }
    }




    void HandleObstacles() {

        Debug.DrawLine(transform.position, transform.position + new Vector3(moveDir * jumpCastLength, 0), Color.red);
        RaycastHit2D r = Physics2D.Raycast(transform.position, new Vector2(moveDir, 0), jumpCastLength, LayerMask.GetMask("Obstacle"));

        if (r.collider != null) {

            Debug.Log(r.collider.gameObject.name);
            if (timeSinceJump >= 0.3f) {
                jump = true;
                timeSinceJump = 0;
            }
        }
        else {
            //jump = false;
        }

    }

    void DoEvasionStuff() {
        if (doRandomJumps) {
            // do random jumps but only when no obstacle is ahead (to avoid jumping on obstacle or just before it later)
            if (timeSinceJump >= jumpCooldown) {

                timeTillRndJump -= Time.deltaTime;

                if (timeTillRndJump <= 0) {
                    RaycastHit2D r = Physics2D.Raycast(transform.position, new Vector2(moveDir, 0), evasionClearDistance, ~(1 << LayerMask.GetMask("Obstacle")));

                    if (r.collider == null) {
                        evasionJump = true;
                        timeSinceJump = 0;
                        Debug.Log("Do evasion jump!");
                    }
                    else {
                        evasionJump = false;
                    }

                    timeTillRndJump = Random.Range(0.5f, 1.0f);
                }
            }
        }
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position - new Vector3(0, 0.2f, 0), 
            transform.position - new Vector3(0, 0.2f, 0) + new Vector3(moveDir * evasionClearDistance, 0, 0));

        Gizmos.DrawWireCube(new Vector3(spawnX, transform.position.y), new Vector3(maxDistToSpawn*2, 2));
    }

    void FixedUpdate() {
        if (moveDir != 0) {
            //rigidbody2D.AddForce(new Vector2(moveForce * moveDir, 0));
            rigidbody2D.velocity = new Vector2(moveForce * moveDir, rigidbody2D.velocity.y);
        }

        if (jump) {
            rigidbody2D.AddForce(new Vector2(0f, jumpStrength));
            jump = false;
        }

        if (evasionJump) {
            rigidbody2D.AddForce(new Vector2(0f, jumpStrength * Random.Range(1.0f, 2.0f)));
            evasionJump = false;
        }

        // saltos, bitch!
        //transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z * 0.9f);
    }
}
