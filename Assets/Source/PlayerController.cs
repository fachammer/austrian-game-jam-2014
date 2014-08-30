using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    // Movement Settings
    public float moveForce = 365f;
    public float rollForce = 150f;
    float curForce;

    public float maxSpeedRolling = 5f;
    public float jumpForce = 1000f;

    public float maxSpeedWalking = 10;

    private float maxSpeed;

    [HideInInspector]
    public bool jump = false;

    [HideInInspector]
    public bool facingRight = false;

    private Transform groundCheck;
    private bool grounded = false;

    bool rollKeyDown = false;

    CircleCollider2D collCircle;
    BoxCollider2D collBox;
    GameObject gun;

    public bool isRolling = false;

    private void Awake() {
        groundCheck = transform.Find("GroundCheck");

        collCircle = GetComponent<CircleCollider2D>();
        collBox = GetComponent<BoxCollider2D>();
        gun = transform.FindChild("Gun").gameObject;
    }

    void Start() {
        maxSpeed = maxSpeedWalking;

        curForce = moveForce;
    }

    private void Update() {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, LayerMask.GetMask("Obstacle", "Ground"));

        if (Input.GetButtonDown("Jump") && grounded) {
            jump = true;
        }

        bool rollKeyPrev = rollKeyDown;
        if (Input.GetButtonDown("Roll") || Input.GetKeyDown(KeyCode.LeftShift)) {
            rollKeyDown = true;
        }
        if (Input.GetButtonUp("Roll") || Input.GetKeyUp(KeyCode.LeftShift)) {
            rollKeyDown = false;
        }

        if (rollKeyDown != rollKeyPrev) {
            if (rollKeyDown) {
                // roll
                isRolling = true;
                collCircle.enabled = true;
                collBox.enabled = false;
                rigidbody2D.fixedAngle = false;
                rigidbody2D.gravityScale = 6f;
                maxSpeed = maxSpeedRolling;
                curForce = rollForce;
            }
            else {
                // dont roll
                isRolling = false;
                collCircle.enabled = false;
                collBox.enabled = true;
                rigidbody2D.fixedAngle = true;
                rigidbody2D.gravityScale = 4f;
                transform.rotation = Quaternion.identity;
                gun.transform.rotation = Quaternion.identity;
                maxSpeed = maxSpeedWalking;
                curForce = moveForce;
            }
        }
    }

    private void FixedUpdate() {
        Vector2 velocity = rigidbody2D.velocity;

        float horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput * rigidbody2D.velocity.x < maxSpeed) {
            rigidbody2D.AddForce(Vector2.right * horizontalInput * curForce);
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