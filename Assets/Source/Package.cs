using UnityEngine;
using System.Collections;

public class Package : MonoBehaviour {

    public GameObject target;

    void Update()
    {
        if (target != null)
        {
            transform.position = target.transform.position + new Vector3(0, 2, 0);
        }
    }
}
