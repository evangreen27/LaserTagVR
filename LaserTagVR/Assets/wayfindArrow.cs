using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wayfindArrow : MonoBehaviour {
    public GameObject Healthpack;
    public GameObject nod;
    public float t = 5f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(t <= 0)
        {
            nod.SetActive(true);
            t = -1f;
        }
        else if (t != -1f)
        {
            t -= Time.deltaTime;
        }
        if (Healthpack)
        {
            transform.LookAt(Healthpack.transform);
            transform.Rotate(Vector3.up, 90f);
        }
	}
}
