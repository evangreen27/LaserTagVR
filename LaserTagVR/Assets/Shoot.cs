using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {


    public int TheDamage =100;
    public int ShotLength;
    public int RayStrength;
    public LineRenderer LaserLine;
    public Light LaserLight;
    public AudioClip blastAudio;
	// Use this for initialization
	void Start () {
        LaserLine.enabled = false;
        //Screen.showCursor = false;
        //Screen.lockCursor = true;
        LaserLight.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
        var forward = transform.TransformDirection(Vector3.forward);

        if (Input.GetKey("r"))
        {

            // Draw the Ray and Laser effects
            Debug.DrawRay(transform.position, forward * ShotLength, Color.blue);
            LaserLine.enabled = true;
            LaserLight.enabled = true;
            //audio.PlayOneShot(blastAudio);

            // Draw raycast and test for a hit
            if (Physics.Raycast(transform.position, forward, 1000.0f, ShotLength))
            {

              
            }

        }
        // turn off the laser and effects
        else
        {
            LaserLine.enabled = false;
            LaserLight.enabled = false;
        }
    }
}
