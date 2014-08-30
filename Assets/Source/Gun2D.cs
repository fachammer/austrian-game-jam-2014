using UnityEngine;

public class Gun2D : MonoBehaviour
{
    public Transform shootPoint;

    public float fireRate = 3;
    public Transform muzzleFire;
    public Transform impact;
    public float minRotation = 0, maxRotation = 360;
    public PlayerController playerController;
    public bool useGamepad;
    public AudioClip shoot;
    private float delay;
    private Vector3 lastMousePos;
    private Vector3 lastGamepad4n5Pos;

    public void TryFire() {
        if (delay <= 0) {
            delay = 1 / fireRate;
            Fire();
        }
    }

    public void Fire() {
        // GameObject muzzleFireGO = Instantiate (muzzleFire, shootPoint.position,
        // shootPoint.rotation) as GameObject; muzzleFireGO.transform.parent = this.transform;

        RaycastHit2D hit = Physics2D.Raycast(new Vector2(shootPoint.position.x, shootPoint.position.y), new Vector2(shootPoint.right.x, shootPoint.right.y), 100f,
            LayerMask.GetMask("Obstacle", "Enemy", "Ground"));
        Vector2 shootPoint2D = new Vector2(shootPoint.position.x, shootPoint.position.y);
        Debug.DrawLine(shootPoint2D, shootPoint2D + new Vector2(shootPoint.right.x, shootPoint.right.y) * 20);

        GetComponent<Animator>().SetTrigger("shoot");
        audio.PlayOneShot(shoot);

        if (hit) {
            Debug.Log(hit.collider.gameObject);
            if (hit.collider.gameObject.layer == 8) {
                Instantiate(impact, hit.point, Quaternion.identity);
            }
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
                hit.collider.GetComponent<Health>().TakeDamage(1);
            }
        }
    }

    private void Start() {
        shootPoint = GameObject.Find(gameObject.name + "/ShootPoint").transform;
    }

    private void Update() {
        if (delay > 0) {
            delay -= Time.deltaTime;
        }

        if (useGamepad) {
            if (Input.mousePosition != lastMousePos) {
                lastMousePos = Input.mousePosition;
                useGamepad = false;
            }
        }
        else {
            if (new Vector3(Input.GetAxis("GamepadX"), Input.GetAxis("GamepadY"), 0) != lastGamepad4n5Pos) {
                lastGamepad4n5Pos = new Vector3(Input.GetAxis("GamepadX"), Input.GetAxis("GamepadY"), 0);
                useGamepad = true;
            }
        }

        float angle, xAxis, yAxis;

        if (useGamepad) {
            xAxis = Input.GetAxis("GamepadX");
            yAxis = Input.GetAxis("GamepadY");
        }
        else {
#if UNITY_STANDALONE
            xAxis = Input.mousePosition.x - Screen.width / 2 - transform.position.x;
            yAxis = Input.mousePosition.y - Screen.height / 2 - transform.position.y;
#endif
#if UNITY_ANDROID
			if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) {
				xAxis = Input.GetTouch(0).deltaPosition;.x - Screen.width/2 - transform.position.x;
				yAxis = Input.GetTouch(0).deltaPosition;.y - Screen.height/2 - transform.position.y;
			}
#endif
        }

        if (!playerController.facingRight) {
            xAxis = -xAxis;
            yAxis = -yAxis;
        }

        if (useGamepad) {
            if (xAxis > 0.1 || xAxis < -0.1 || yAxis > 0.1 || yAxis < -0.1) {
                angle = Mathf.Atan2(xAxis, yAxis) * Mathf.Rad2Deg + 90;
            }
            else {
                angle = Mathf.Atan2(xAxis, 0) * Mathf.Rad2Deg + 90;
            }
        }
        else {
            angle = -Mathf.Atan2(xAxis, yAxis) * Mathf.Rad2Deg - 90;
        }

        // if (angle > minRotation-90 && angle < maxRotation-90) {
        if (playerController.facingRight) {
            transform.localEulerAngles = new Vector3(0, 0, -angle);
        }
        else {
            transform.localEulerAngles = new Vector3(0, 0, angle);
        }
        // }

        if (Input.GetButton("Fire1") || Input.GetAxis("FireJoystick") < -0.1) {
            TryFire();
        }
    }
}