using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tags : MonoBehaviour {

    public List<string> tags;

    void Start()
    {
        tags = new List<string>();
        tags.Add("Player");
    }
    
}
