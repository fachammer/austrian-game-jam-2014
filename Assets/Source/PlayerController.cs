using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    // Movement Settings
    public float moveForce = 365f;

    public float rollForce = 150f;
    public float maxSpeedRolling = 5f;
    public float walkJumpForce = 2000f;
    public float rollJumpForce = 3000f;
    float jumpForce;

    public float maxSpeedWalking = 10;

    [HideInInspector]
    public bool jump = false;

    [HideInInspector]
    public bool facingLeft = false;
    [HideInInspector]
    public bool isRolling = false;
    private float curForce;
    private float maxSpeed;
    private bool grounded = false;

    private bool rollKeyDown = false;

    [HideInInspector]
    public bool isKnockedBack = false;

    private CircleCollider2D collCircle;
    private BoxCollider2D collBox;
    private GameObject gun;

    private void Awake() {

        collCircle = GetComponent<CircleCollider2D>();
        collBox = GetComponent<BoxCollider2D>();
        gun = transform.FindChild("Gun").gameObject;
    }

    private void Start() {
        maxSpeed = maxSpeedWalking;

        curForce = moveForce;
        jumpForce = walkJumpForce;
    }

    public void StopKnockback() {
        isKnockedBack = false;
    }

    public void StartKnockback() {
        isKnockedBack = true;
        Invoke("StopKnockback", 0.5f);
        GameObject.Find("CameraWrapper").GetComponent<Shake>().DoShake(0.3f);
        Invoke("StopKnockback", 0.5f);
    }

    private void Update() {
        grounded = Physics2D.Linecast(transform.position, 
            new Vector2(transform.position.x, transform.position.y) + new Vector2(0,-3), 
            LayerMask.GetMask("Obstacle", "Ground"));

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
                jumpForce = rollJumpForce;

                //Camera.main.GetComponent<AudioSource>().volume = 1.0f;
                //GameObject.Find("CalmMusic").GetComponent<AudioSource>().volume = 0.0f;
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
                jumpForce = walkJumpForce;

                //Camera.main.GetComponent<AudioSource>().volume = 0.0f;
                //GameObject.Find("CalmMusic").GetComponent<AudioSource>().volume = 1.0f;
            }
        }

        if (isRolling) {
            //Camera.main.GetComponent<Shake>().DoShake(0.01f);
            GameObject.Find("CameraWrapper").GetComponent<Shake>().DoShake(0.1f);
        }
    }

    private void FixedUpdate() {

        if (!isKnockedBack) {
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
        }
        // GetComponent<Animator>().SetInteger("directionX", (int)rigidbody2D.velocity.x);
    }

    private void Flip() {
        facingLeft = !facingLeft;

        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }
}