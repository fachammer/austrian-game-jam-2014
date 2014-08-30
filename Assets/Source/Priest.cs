using UnityEngine;
using System.Collections;

public class Priest : MonoBehaviour {

    public float floatSpeed = 0.2f;
    public float maxMoveDist = 5;
    public float changeTargetTime = 0.5f;

    Vector3 moveDir;
    Vector3 targetPoint;
    Vector3 spawnPoint;



    float changeCooldown = 0;

	// Use this for initialization
	void Start () {
        spawnPoint = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        Wander();
	}

    void Wander(){

        changeCooldown -= Time.deltaTime;

        if (changeCooldown <= 0) {
            changeCooldown = changeTargetTime;
            //moveDir = Quaternion.Euler(0, 0, Random.Range(0, 360)) * new Vector3(1, 1, 0);
            //moveDir.Normalize();

            targetPoint = spawnPoint + new Vector3(
                    Random.Range(-maxMoveDist, maxMoveDist),
                    Random.Range(-maxMoveDist, maxMoveDist),
                    0
                );

            moveDir = targetPoint - transform.position;
            moveDir.Normalize();
        }
    }

    void FixedUpdate() {
        //rigidbody2D.AddForce(new Vector2(moveDir.x, moveDir.y) * floatSpeed);
        rigidbody2D.velocity = new Vector2(moveDir.x, moveDir.y) * floatSpeed;
        Debug.Log(new Vector2(moveDir.x, moveDir.y) * floatSpeed);
    }

    void OnDrawGizmos() {
        Gizmos.DrawLine(transform.position, targetPoint);
        Gizmos.DrawWireCube(spawnPoint, new Vector3(maxMoveDist * 2, maxMoveDist * 2));
        Gizmos.DrawSphere(targetPoint, 0.5f);
    }
}
