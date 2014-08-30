using UnityEngine;
using System.Collections;

public class CameraFollow2D : MonoBehaviour 
{
	public Transform target;

	void FixedUpdate ()
	{

        transform.position = target.transform.position + new Vector3(0.0f, 2.0f, -20.0f);
	}

}
