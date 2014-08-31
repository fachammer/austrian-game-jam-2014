using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float timeToLive;
    public float velocity;

    public static GameObject Instantiate(GameObject original, float direction) {
        GameObject instance = (GameObject)GameObject.Instantiate(original);
        Projectile projectile = instance.GetComponent<Projectile>();
        projectile.velocity = Mathf.Sign(direction) * projectile.velocity;
        return instance;
    }

    private void Start() {
        Destroy(gameObject, timeToLive);
        rigidbody2D.velocity = new Vector2(velocity, 0f);

    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log("on collision enter");
        if (collision.transform.name == "Player") {
            collision.gameObject.GetComponent<Health>().TakeDamage(1);
            Destroy(gameObject);
        }
    }
}