using UnityEngine;

public class BloodEffects : MonoBehaviour
{
    private BloodEffect[] effects;

    private bool previousMouse;

    public void Stimulate() {
        int randomIndex = Random.Range(0, effects.Length);
        effects[randomIndex].Stimulate();
    }

    private void Start() {
        effects = GetComponentsInChildren<BloodEffect>();
    }

    private void Update() {
        bool currentMouse = Input.GetMouseButton(0);
        if (!previousMouse && currentMouse)
            Stimulate();

        previousMouse = currentMouse;
    }
}