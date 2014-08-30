using UnityEngine;

public class BloodEffects : MonoBehaviour
{
    public static BloodEffects Instance { get { return instance; } }
    private static BloodEffects instance;

    private BloodEffect[] effects;

    private bool previousMouse;

    public void Stimulate() {
        int randomIndex = Random.Range(0, effects.Length);
        effects[randomIndex].Stimulate();
    }

    void Awake() {
        instance = this;
    }
    private void Start() {
        effects = GetComponentsInChildren<BloodEffect>();
    }

    //private void Update() {
    //    bool currentMouse = Input.GetMouseButton(0);
    //    if (!previousMouse && currentMouse)
    //        Stimulate();

    //    previousMouse = currentMouse;
    //}
}