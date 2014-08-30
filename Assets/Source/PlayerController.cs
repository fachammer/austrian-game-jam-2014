using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float speed = 10f;

    // Movement Settings
    public float moveForce = 365f;

    public float maxSpeed = 5f;
    public float jumpForce = 1000f;

    [HideInInspector]
    public bool jump = false;

    [HideInInspector]
    public bool facingRight = false;

    private Transform groundCheck;
    private bool grounded = false;

    bool rollKeyDown = false;

    private void Awake() {
        groundCheck = transform.Find("GroundCheck");
    }

    private void Update() {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if (Input.GetButtonDown("Jump") && grounded) {
            jump = true;
        }

        if (Input.GetButtonDown("Roll") || Input.GetKeyDown(KeyCode.LeftShift)) {
            rollKeyDown = true;
        }
        if (Input.GetButtonUp("Roll") || Input.GetKeyUp(KeyCode.LeftShift)) {
            rollKeyDown = false;
        }


        if (rollKeyDown) {
            Debug.Log("rolll");
            GetComponent<CircleCollider2D>().enabled = true;
            GetComponent<BoxCollider2D>().enabled = false;
        }
        else {
            GetComponent<CircleCollider2D>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    private void FixedUpdate() {
        Vector2 velocity = rigidbody2D.velocity;

        float horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput * rigidbody2D.velocity.x < maxSpeed) {
            rigidbody2D.AddForce(Vector2.right * horizontalInput * moveForce);
        }

        if (Mathf.Abs(rigidbody2D.velocity.x) > maxSpeed) {
            rigidbody2D.velocity = new Vector2(Mathf.Sign(rigidbody2D.velocity.x) * maxSpeed, rigidbody2D.velocity.y);
        }

        if (horizontalInput > 0 && facingRight)
            Flip();
        else if (horizontalInput < 0 && !facingRight)
            Flip();

        if (jump) {
            rigidbody2D.AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }

        GetComponent<Animator>().SetInteger("directionX", (int)rigidbody2D.velocity.x);
    }

    private void Flip() {
        facingRight = !facingRight;

        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }
}