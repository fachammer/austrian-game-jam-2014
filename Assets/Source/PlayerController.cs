using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    // Movement Settings
    public float moveForce = 365f;

    public float rollForce = 150f;
    public float maxSpeedRolling = 5f;
    public float jumpForce = 1000f;
    public float maxSpeedWalking = 10;

    [HideInInspector]
    public bool jump = false;

    [HideInInspector]
    public bool facingLeft = false;

    public bool isRolling = false;
    private float curForce;
    private float maxSpeed;
    private Transform groundCheck;
    private bool grounded = false;

    private bool rollKeyDown = false;

    private CircleCollider2D collCircle;
    private BoxCollider2D collBox;
    private GameObject gun;

    private void Awake() {
        groundCheck = transform.Find("GroundCheck");

        collCircle = GetComponent<CircleCollider2D>();
        collBox = GetComponent<BoxCollider2D>();
        gun = transform.FindChild("Gun").gameObject;
    }

    private void Start() {
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
                Debug.Log("roll");
                isRolling = true;
                //collCircle.enabled = true;
                //collBox.enabled = false;
                rigidbody2D.fixedAngle = false;
                rigidbody2D.gravityScale = 6f;
                maxSpeed = maxSpeedRolling;
                curForce = rollForce;

                Camera.main.GetComponent<AudioSource>().volume = 0.2f;
                GameObject.Find("CalmMusic").GetComponent<AudioSource>().volume = 0.0f;
            }
            else {
                // dont roll
                isRolling = false;
                //collCircle.enabled = false;
                //collBox.enabled = true;
                rigidbody2D.fixedAngle = true;
                rigidbody2D.gravityScale = 4f;
                transform.rotation = Quaternion.identity;
                gun.transform.rotation = Quaternion.identity;
                maxSpeed = maxSpeedWalking;
                curForce = moveForce;


                Camera.main.GetComponent<AudioSource>().volume = 0.0f;
                GameObject.Find("CalmMusic").GetComponent<AudioSource>().volume = 0.5f;
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

        if (horizontalInput > 0 && facingLeft)
            Flip();
        else if (horizontalInput < 0 && !facingLeft)
            Flip();

        if (jump) {
            rigidbody2D.AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }

        GetComponent<Animator>().SetInteger("directionX", (int)rigidbody2D.velocity.x);
    }

    private void Flip() {
        facingLeft = !facingLeft;

        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }
}