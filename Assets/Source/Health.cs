using UnityEngine;

public class Health : MonoBehaviour
{
    public delegate void HealthHandler();
    public event HealthHandler OnHit, OnDeath;

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
            

            if (OnHit != null) OnHit();

            animator.SetInteger(healthAnimationParameter, CurrentHealth);
            if (CurrentHealth <= 0) {
                if (OnDeath != null) OnDeath();
                Destroy(gameObject, remainTimeAfterDeath);
            }
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