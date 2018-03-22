using UnityEngine;
using System.Collections;

public class TeleBehavior : MonoBehaviour
{

    Vector3 oldpos;
    public GameObject player;
    // Use this for initialization
    void Start()
    {
        oldpos = transform.position;
        player = GameObject.Find("OVRCameraRig");
    }

    // Update is called once per frame
    void Update()
    {
        oldpos = transform.position;
        transform.position += transform.forward * Time.deltaTime * 10f;
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.CompareTag("teleable"))
        {
            print(collisionInfo.collider.gameObject);
            float y = player.transform.position.y;
            player.transform.position = new Vector3(collisionInfo.contacts[0].point.x, collisionInfo.contacts[0].point.y + 5, collisionInfo.contacts[0].point.z);
            Destroy(this);
        }
    }
}
