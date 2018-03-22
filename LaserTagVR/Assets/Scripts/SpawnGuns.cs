using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGuns : MonoBehaviour {

    public GameObject AK;
    public GameObject M4;
    public GameObject pistol;
    public GameObject UMP;

    // Use this for initialization
    void Start () {
        GameObject.Instantiate(AK, new Vector3(Random.Range(-100, 110), 30, Random.Range(100, -100)), Quaternion.Euler(0, 0, 0));
        GameObject.Instantiate(M4, new Vector3(Random.Range(-100, 110), 30, Random.Range(100, -100)), Quaternion.Euler(0, 0, 0));
        GameObject.Instantiate(pistol, new Vector3(Random.Range(-100, 110), 30, Random.Range(100, -100)), Quaternion.Euler(0, 0, 0));
        GameObject.Instantiate(UMP, new Vector3(Random.Range(-100, 110), 30, Random.Range(100, -100)), Quaternion.Euler(0, 0, 0));
    }
}
