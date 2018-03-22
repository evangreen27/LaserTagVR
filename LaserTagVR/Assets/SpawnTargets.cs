using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTargets : MonoBehaviour {

    public GameObject TargetPrefab;
    public float timeToNext = 5.0f;
    public float spawnrate;
    public float targetsShot = 1;
    public float targetSize;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(timeToNext <= 0)
        {
            GameObject targ = GameObject.Instantiate(TargetPrefab, new Vector3(Random.Range(-100.0f, 100.0f), Random.Range(3f, 30.0f), Random.Range(-100.0f, 100.0f)), Quaternion.Euler(0, 0, 0));
            targ.transform.localScale = new Vector3(targetSize / 5, targetSize / 5, targetSize / 5);
            timeToNext = spawnrate;
        }
        else
        {
            timeToNext -= Time.deltaTime;
        }
	}
}
