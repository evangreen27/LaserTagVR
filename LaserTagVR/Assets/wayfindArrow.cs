using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wayfindArrow : MonoBehaviour {
    public GameObject Healthpack;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Healthpack)
        {
            transform.LookAt(Healthpack.transform);
            transform.Rotate(Vector3.up, 90f);
        }
	}
}
