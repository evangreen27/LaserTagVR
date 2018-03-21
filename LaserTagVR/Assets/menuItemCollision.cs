using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class menuItemCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.name != "laser")
        //{
        //    GameObject dropdown = other.gameObject;
        //    if (this.transform.childCount != 3)
        //    {
        //        this.GetComponent<Dropdown>().Hide();
        //    }
        //    else
        //    {
        //        this.GetComponent<Dropdown>().Show();
        //    }
        //}
    }
}
