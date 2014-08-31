using System.Collections;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    public float velocity;

    public float jumpProbability;
    public float jumpCheckInterval;
    public float jumpForce;

    public float shootProbability;
    public float shootCheckInterval;
    public GameObject projectile;
    public Transform shootPoint;

    private float jumpCheckTimer;
    private float shootCheckTimer;
    private Transform playerTransform;

    private void Start() {
        playerTransform = GameObject.Find("Player").transform;
        GetComponent<Health>().OnDeath += new Health.HealthHandler(BossBehaviour_OnDeath);
    }

    void BossBehaviour_OnDeath() {
        Application.LoadLevel("menu");
    }

    private void Update() {
        DoFacing();
        DoMoving();
        DoJumping();
        DoShooting();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.transform == playerTransform) {
            playerTransform.GetComponent<Health>().TakeDamage(1);
            StopAllCoroutines();
            StartCoroutine(DashAway());
        }
    }

    private IEnumerator DashAway() {
        float xDirection = playerTransform.position.x - transform.position.x;

        for (int i = 0; i < 10; i++) {
            rigidbody2D.velocity = new Vector2(-xDirection * velocity * 5, rigidbody2D.velocity.y);
            yield return null;
        }

        if (!IsJumping())
            Jump();
    }

    private void DoFacing() {
        if (IsFacingAwayFromPlayer())
            Flip();
    }

    private bool IsFacingAwayFromPlayer() {
        return playerTransform.position.x >= transform.position.x && transform.localScale.x > 0 ||
                playerTransform.position.x < transform.position.x && transform.localScale.x < 0;
    }

    private void Flip() {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    private void DoMoving() {
        float xDirection = playerTransform.position.x - transform.position.x;
        rigidbody2D.velocity = new Vector2(xDirection * velocity, rigidbody2D.velocity.y);
    }

    private void DoJumping() {
        //if (!IsJumping()) 
        {
            jumpCheckTimer += Time.deltaTime;

            if (jumpCheckTimer >= jumpCheckInterval) {
                if (ShouldJump())
                    Jump();

                jumpCheckTimer = 0f;
            }
        }
    }

    private bool IsJumping() {
        return Physics2D.Raycast(transform.position, -Vector2.up * 6.0f, 2, LayerMask.GetMask("Ground")).collider != null;
    }

    private bool ShouldJump() {
        return Random.value <= jumpProbability;
    }

    private void Jump() {
        rigidbody2D.AddForce(Vector2.up * jumpForce);
    }

    private void DoShooting() {
        shootCheckTimer += Time.deltaTime;

        if (shootCheckTimer >= shootCheckInterval) {
            if (ShouldShoot())
                Shoot();

            shootCheckTimer = 0f;
        }
    }

    private bool ShouldShoot() {
        return Random.value <= shootProbability;
    }

    private void Shoot() {
        GameObject instance = Projectile.Instantiate(projectile, playerTransform.position.x - transform.position.x);
        instance.transform.position = shootPoint.position;
    }
}