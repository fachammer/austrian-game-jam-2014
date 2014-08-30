using UnityEngine;

public class Health : MonoBehaviour
{
    public int initialHealth;
    public float remainTimeAfterDeath;
    public string healthAnimationParameter;

    private Animator animator;
    private BloodEffects bloodEffects;

    public int CurrentHealth { get; private set; }

    public void TakeDamage(int damage) {
        if (CurrentHealth > 0 && damage > 0) {
            CurrentHealth -= damage;
            bloodEffects.Stimulate();

            animator.SetInteger(healthAnimationParameter, CurrentHealth);
        }
        else if (CurrentHealth <= 0) {
            Destroy(gameObject, remainTimeAfterDeath);
        }
    }

    private void Awake() {
        animator = GetComponent<Animator>();
        bloodEffects = GameObject.FindObjectOfType<BloodEffects>();
    }

    private void Start() {
        CurrentHealth = initialHealth;
        animator.SetInteger(healthAnimationParameter, CurrentHealth);
    }
}