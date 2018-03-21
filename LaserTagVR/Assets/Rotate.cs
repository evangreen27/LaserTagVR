using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {
    public GameObject arrow;
	// Use this for initialization
	void Start () {
        arrow = GameObject.Find("WayfindArrow");
        arrow.GetComponent<wayfindArrow>().Healthpack = this.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        transform.RotateAround(transform.position, transform.up, -75 * Time.deltaTime);
    }
}
