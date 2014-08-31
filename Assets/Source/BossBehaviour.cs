using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    public float velocity;

    public float jumpProbability;
    public float jumpCheckInterval;
    public float jumpForce;

    private float jumpCheckTimer;
    private Transform playerTransform;

    private void Start() {
        playerTransform = GameObject.Find("Player").transform;
    }

    private void Update() {
        DoFacing();
        DoMoving();
        DoJumping();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.transform == playerTransform) {
            playerTransform.GetComponent<Health>().TakeDamage(1);
            float xDirection = playerTransform.position.x - transform.position.x;
            rigidbody2D.velocity = new Vector2(-xDirection * velocity * 1000, rigidbody2D.velocity.y);
        }
    }

    private void DoFacing() {
        if (IsFacingAwayFromPlayer())
            Flip();
    }

    private bool IsFacingAwayFromPlayer() {
        return playerTransform.position.x >= transform.position.x && transform.localScale.x < 0 ||
                playerTransform.position.x < transform.position.x && transform.localScale.x > 0;
    }

    private void Flip() {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    private void DoMoving() {
        float xDirection = playerTransform.position.x - transform.position.x;
        rigidbody2D.velocity = new Vector2(xDirection * velocity, rigidbody2D.velocity.y);
    }

    private void DoJumping() {
        Debug.DrawLine(transform.position, transform.position - Vector3.up * 2.8f, Color.green);
        if (!IsJumping()) {
            jumpCheckTimer += Time.deltaTime;

            if (jumpCheckTimer >= jumpCheckInterval) {
                if (ShouldJump())
                    Jump();

                jumpCheckTimer = 0f;
            }
        }
    }

    private bool IsJumping() {
        return Physics2D.Raycast(transform.position, -Vector2.up, 2, LayerMask.GetMask("Ground")).collider != null;
    }

    private bool ShouldJump() {
        return Random.value <= jumpProbability;
    }

    private void Jump() {
        rigidbody2D.AddForce(Vector2.up * jumpForce);
    }
}