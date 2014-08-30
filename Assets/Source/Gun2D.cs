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

        float angle;
        if (useGamepad) {
            float controllerX = Input.GetAxis("GamepadX");
            float controllerY = Input.GetAxis("GamepadY");
            angle = Mathf.Atan2(controllerY, controllerX) * Mathf.Rad2Deg;
        }
        else {
            Vector2 difference = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            Debug.Log(difference);
            angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        }

        if (playerController.facingLeft) {
            transform.eulerAngles = new Vector3(0, 0, 180 - angle);
            transform.FindChild("ShootPoint").localEulerAngles = new Vector3(0, 0, -(90 - angle) * 2);
        }
        else {
            transform.eulerAngles = new Vector3(0, 0, angle);
            transform.FindChild("ShootPoint").localEulerAngles = new Vector3(0, 0, 0);
        }

        if (Input.GetButton("Fire1") || Input.GetAxis("FireJoystick") < -0.1) {
            TryFire();
        }
    }
}