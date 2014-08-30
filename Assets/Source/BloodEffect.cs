using UnityEngine;
using UnityEngine.UI;

public class BloodEffect : MonoBehaviour
{
    public float fadingDuration;

    private float alphaFadeTimer;
    private RawImage image;

    public void Stimulate() {
        alphaFadeTimer = fadingDuration;
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
    }

    private void Start() {
        image = GetComponent<RawImage>();
    }

    private void Update() {
        if (alphaFadeTimer > 0) {
            alphaFadeTimer -= Time.deltaTime;
            Color c = image.color;
            c.a = alphaFadeTimer / fadingDuration - 0.0f;
            image.color = c;
        }
    }
}