using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTargets : MonoBehaviour {

    public GameObject TargetPrefab;
    public float prev = 5.0f;
    public float timeToNext = 5.0f;
    public float targetsShot = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(timeToNext <= 0)
        {
            GameObject.Instantiate(TargetPrefab, new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(9.5f, 10.0f), Random.Range(-10.0f, 10.0f)), Quaternion.Euler(0, 0, 0));
            if(prev > 0.4f)
                prev -= 0.2f;
            timeToNext = prev;
        }
        else
        {
            timeToNext -= Time.deltaTime;
        }
	}
}
