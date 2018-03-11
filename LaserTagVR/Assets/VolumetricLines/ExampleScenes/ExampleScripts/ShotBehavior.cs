using UnityEngine;
using System.Collections;

public class ShotBehavior : MonoBehaviour {

    Vector3 oldpos;
    // Use this for initialization
    void Start () {
        oldpos = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        oldpos = transform.position;
        transform.position += transform.forward * Time.deltaTime * 10f;
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        Vector3 refl = collisionInfo.contacts[0].normal;
        transform.forward = Vector3.Reflect((transform.position - oldpos).normalized, refl);
        print(collisionInfo.collider.gameObject.name);
    }
}
