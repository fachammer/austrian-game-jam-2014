using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{
    public float minXForce = 100f;
    public float maxXForce = 300f;
    public float minYForce = 150f;
    public float maxYForce = 300f;
    public float maxRotForce = 300.0f;
    public float minRotForce = 100.0f;

    public AudioClip deathSound;
    public AudioClip hitSound;
    public Text gameOverObject;
    public Text gamepadHint;
    public Image restartButton;
    public Image quitButton;
    public Image backGround;
    public SpriteRenderer XBoxA;
    public SpriteRenderer XBoxB;
    public GameObject packagePrefab;

    private AudioSource audioSource;
    private PlayerController playerController;
    private Gun2D gun2D;
    private Health health;
    private GameObject bloodEffects;

    private void Start() {
        audioSource = transform.GetComponent<AudioSource>();
        playerController = GetComponent<PlayerController>();
        gun2D = transform.FindChild("Gun").GetComponent<Gun2D>();
        health = GetComponent<Health>();
        bloodEffects = GameObject.Find("bloodEffects");

        hideGUI();

        health.OnHit += new Health.HealthHandler(health_OnHit);
        health.OnDeath += new Health.HealthHandler(health_OnDeath);
    }

    void health_OnDeath() {
        Death();
    }

    void health_OnHit() {
        audioSource.clip = hitSound;
        audioSource.Play();
    }
    public void Hit()
    {
        //losePackage();
        health.TakeDamage(1);

        //if (health.CurrentHealth <= 0)
        //{
        //    Death();
        //}
        //else
        //{
        //    audioSource.clip = hitSound;
        //    audioSource.Play();
        //}
    }

    private void Death() {
        playerController.enabled = false;
        gun2D.enabled = false;

        audioSource.clip = deathSound;
        audioSource.Play();

        showGUI(gun2D.useGamepad);
    }

    private void losePackage() {
        GameObject package = (GameObject)Instantiate(packagePrefab, transform.position, Quaternion.identity);

        float xForce = Random.Range(0, 2) == 0 ? Random.Range(-maxXForce, -minXForce) : Random.Range(minXForce, maxXForce);
        float yForce = Random.Range(minYForce, maxYForce);
        float rotForce = Random.Range(0, 2) == 0 ? Random.Range(-maxRotForce, -minRotForce) : Random.Range(minRotForce, maxRotForce);

        package.rigidbody2D.AddForce(new Vector2(xForce, yForce));
        package.rigidbody2D.AddTorque(rotForce);
    }

    private void hideGUI() {
        backGround.enabled = false;
        gameOverObject.enabled = false;
        restartButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
        gamepadHint.enabled = false;
        XBoxA.enabled = false;
        XBoxB.enabled = false;
    }

    private void showGUI(bool gamePad) {
        bloodEffects.SetActive(false);
        backGround.enabled = true;
        gameOverObject.enabled = true;
        if (gamePad) {
            gamepadHint.enabled = true;
            XBoxA.enabled = true;
            XBoxB.enabled = true;
        }
        else {
            restartButton.gameObject.SetActive(true);
            quitButton.gameObject.SetActive(true);
        }
    }
}