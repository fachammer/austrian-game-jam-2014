using UnityEngine;
using UnityEngine.UI;

public class BloodEffect : MonoBehaviour
{
    public float fadingDuration;

    private float alphaFadeTimer;
    private RawImage image;

    public void Stimulate() {
        alphaFadeTimer = fadingDuration;
    }

    private void Start() {
        image = GetComponent<RawImage>();
    }

    private void Update() {
        if (alphaFadeTimer > 0) {
            Debug.Log(alphaFadeTimer);
            alphaFadeTimer -= Time.deltaTime;
            Color c = image.color;
            c.a = alphaFadeTimer / fadingDuration;
            image.color = c;
        }
    }
}