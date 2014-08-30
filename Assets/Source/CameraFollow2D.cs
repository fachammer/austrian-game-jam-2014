using UnityEngine;
using System.Collections;

public class CameraFollow2D : MonoBehaviour 
{
	public Transform target;

	public float xMargin = 1f;		
	public float yMargin = 1f;	
	public float xSmooth = 8f;	
	public float ySmooth = 8f;	

	void FixedUpdate ()
	{
		TrackPlayer();
	}
	
	
	void TrackPlayer ()
	{
		float targetX = transform.position.x;
		float targetY = transform.position.y;

		if(Mathf.Abs(transform.position.x - target.position.x) > xMargin)
			targetX = Mathf.Lerp(transform.position.x, target.position.x, xSmooth * Time.deltaTime);

		if(Mathf.Abs(transform.position.y - target.position.y) > yMargin)
			targetY = Mathf.Lerp(transform.position.y, target.position.y, ySmooth * Time.deltaTime);

		transform.position = new Vector3(targetX, targetY, transform.position.z);
	}
}
