using UnityEngine;

public class Woman : MonoBehaviour
{
    public float moveForce;
    public float jumpStrength;
    public float grabDistance;
    public float jumpCooldown;
    public float evasionClearDistance;
    public float jumpCastLength = 1f;

    public GameObject package;

    public AudioClip[] screams;

    private bool hasAttacked = false;
    private int moveDir = 0;
    private bool jump = false;
    private bool evasionJump = false;
    private float timeTillRndJump;

    private float timeSinceJump = 0;
    private Collider2D colliders;

    //private GameObject player;
    PlayerController player;

    private void Awake() {
        player = GameObject.Find("Player").GetComponent<PlayerController>();

        Collider2D[] playerColls = player.GetComponents<Collider2D>();
        foreach (Collider2D c in playerColls) {
            Physics2D.IgnoreCollision(collider2D, c);
        }

        GetComponent<Health>().OnDeath += new Health.HealthHandler(Woman_OnDeath);
    }

    void Woman_OnDeath() {
        Scream();
        BloodEffects.Instance.Stimulate();
    }

    private void Start() { 
        timeTillRndJump = Random.Range(0.5f, 3.0f);
    }

    private void Update()
    {
        if (!hasAttacked)
        {
            Attack();
        }
        else
        {
            Escape();
        }

        timeSinceJump += Time.deltaTime;
        HandleObstacles();
        DoEvasionStuff();
        CheckDispose();
    }

    private void Attack()
    {
        Vector3 targetPos = player.transform.position;
        if (targetPos.x < transform.position.x)
        {
            moveDir = -1;
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else
        {
            moveDir = 1;
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        TryHitPlayer();
    }

    private void TryHitPlayer()
    {
        if (Mathf.Abs(player.transform.position.x - transform.position.x) <= grabDistance &&
            Mathf.Abs(player.transform.position.y - transform.position.y) <= grabDistance)
        {
            if (player.isRolling) {
                ThrowAway();
            }
            else {
                hasAttacked = true;
                player.GetComponent<PlayerBehaviour>().Hit();
                TakePackage();
            }
        }
    }
    public void ThrowAway()
    {
        rigidbody2D.fixedAngle = false;
        GetComponent<BoxCollider2D>().enabled = false;
        if (moveDir >= 0)
        {
            rigidbody2D.AddForce(new Vector2(Random.Range(-500, -1000), Random.Range(2000, 5000)));
        }
        else
        {
            rigidbody2D.AddForce(new Vector2(Random.Range(500, 1000), Random.Range(1500, 4000)));
        }
        rigidbody2D.AddTorque(50);
        DestroyThisTimed dtt = gameObject.AddComponent<DestroyThisTimed>();
        dtt.time = 5;
        GetComponent<Health>().enabled = false;
        this.enabled = false;
    }

    private void TakePackage()
    {
        GameObject pkg = (GameObject)Instantiate(package, transform.position + new Vector3(0, 2, 0), Quaternion.identity);
        pkg.GetComponentInParent<Package>().target = gameObject;
    }

    private void Escape()
    {
        moveDir = -1;
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }
    private void CheckDispose()
    {
        if (transform.position.x < player.transform.position.x)
        {
            float sqrDist = (player.transform.position - transform.position).sqrMagnitude;

            if (sqrDist >= 250000)
            {
                Destroy(gameObject);
            }
        }
    }

    private void HandleObstacles() {
        Debug.DrawLine(transform.position, transform.position + new Vector3(moveDir * jumpCastLength, 0), Color.red);
        RaycastHit2D r = Physics2D.Raycast(transform.position, new Vector2(moveDir, 0), jumpCastLength, LayerMask.GetMask("Obstacle"));

        if (r.collider != null) {
            if (timeSinceJump >= 0.3f) {
                jump = true;
                timeSinceJump = 0;
            }
        }
        else {
            //jump = false;
        }
    }

    private void DoEvasionStuff() {
        // do random jumps but only when no obstacle is ahead (to avoid jumping on obstacle or just
        // before it later)
        if (timeSinceJump >= jumpCooldown) {
            timeTillRndJump -= Time.deltaTime;

            if (timeTillRndJump <= 0) {
                RaycastHit2D r = Physics2D.Raycast(transform.position, new Vector2(moveDir, 0), evasionClearDistance, ~(1 << LayerMask.GetMask("Obstacle")));

                if (r.collider == null) {
                    evasionJump = true;
                    timeSinceJump = 0;
                }
                else {
                    evasionJump = false;
                }

                timeTillRndJump = Random.Range(0.5f, 1.0f);
            }
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, grabDistance);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position - new Vector3(0, 0.2f, 0),
            transform.position - new Vector3(0, 0.2f, 0) + new Vector3(moveDir * evasionClearDistance, 0, 0));
    }

    private void FixedUpdate() {
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
        transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z * 0.9f);
    }

    void Scream() {
        //audio.clip = screams[Random.Range(0, screams.Length - 1)];
        //audio.Play();
        AudioSource.PlayClipAtPoint(screams[Random.Range(0, screams.Length - 1)], transform.position);
    }
}