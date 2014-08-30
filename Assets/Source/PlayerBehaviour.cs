using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour {

    public int initialPackageCount = 3;
    public float minXForce = 10.0f;
    public float maxXForce = 15.0f;
    public float minYForce = 10.0f;
    public float maxYForce = 15.0f;
    public AudioClip deathSound;
    public AudioClip hitSound;
    public GameObject gameOverObject;
    public GameObject restartButton;
    public GameObject quitButton;
    public GameObject gamepadRestartQuitHint;
    public GameObject packagePrefab;

    private new Light light;
    private AudioSource audioSource;
    private PlayerController playerController;
    private Gun2D gun2D;

    public int packageCount
    {
        get { return packageCount; }
        set { packageCount = value; }
    }

    void Start()
    {
        packageCount = initialPackageCount;
        audioSource = transform.GetComponent<AudioSource>();
        light = GameObject.FindGameObjectWithTag(Tags.Light).GetComponent<Light>();
        playerController = GetComponent<PlayerController>();
        gun2D = transform.FindChild("Gun").GetComponent<Gun2D>();

        gameOverObject.GetComponent<Text>().enabled = false;
        restartButton.GetComponent<Button>().enabled = false;
        quitButton.GetComponent<Button>().enabled = false;
        gamepadRestartQuitHint.GetComponent<Text>().enabled = false;
    }

    public void Hit() 
    {
        packageCount--;
        if (packageCount == 0)
        {
            Death();
        }
        else
        {
            audioSource.clip = hitSound;
            audioSource.Play();
            losePackage();
        }
    }

    private void Death()
    {
        playerController.enabled = false;
        gun2D.enabled = false;

        audioSource.clip = deathSound;
        audioSource.Play();

        light.intensity = 0.1f;
        gameOverObject.GetComponent<Text>().enabled = true;
        if (gun2D.useGamepad)
        {
            gamepadRestartQuitHint.GetComponent<Text>().enabled = true;
        }
        else
        {
            restartButton.GetComponent<Button>().enabled = true;
            quitButton.GetComponent<Button>().enabled = true;
        }
    }

    private void losePackage()
    {
        GameObject package = (GameObject)Instantiate(packagePrefab, transform.position, Quaternion.identity);

        float xForce = (Random.Range(0, 2) == 0 ? Random.Range(-maxXForce, -minXForce) : Random.Range(minXForce, maxXForce));
        float yForce = Random.Range(minYForce, maxYForce);
        package.rigidbody2D.AddForce(new Vector2(xForce, yForce), ForceMode2D.Impulse);
    }
}
