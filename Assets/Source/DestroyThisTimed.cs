using UnityEngine;
using System.Collections;

public class DestroyThisTimed : MonoBehaviour {

	public float time = 0.1f;

	private float timer;

	void Update () {
	
		timer += Time.deltaTime;

		if (timer >= time) {
			Destroy(gameObject);
		}

	}
}
