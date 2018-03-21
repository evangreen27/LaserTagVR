using UnityEngine;
using UnityEngine.UI;
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
        if (!collisionInfo.gameObject.CompareTag("ignore"))
        {
            if (collisionInfo.gameObject.CompareTag("target"))
            {
                GameObject scoretext = GameObject.Find("scoretext");
                float pts = GameObject.Find("ControlObjects").GetComponent<SpawnTargets>().targetsShot++;
                scoretext.GetComponent<UnityEngine.UI.Text>().text = "Score: " + pts;
                Destroy(collisionInfo.collider.gameObject);
                DestroyImmediate(this.gameObject);
            }
            else if(collisionInfo.gameObject.name == "Dropdown")
            {
                Physics.IgnoreCollision(collisionInfo.collider, this.GetComponent<Collider>());
                GameObject dropdown = collisionInfo.collider.gameObject;
                if (dropdown.transform.childCount != 3)
                {
                    dropdown.GetComponent<Dropdown>().Hide();
                } else
                {
                    dropdown.GetComponent<Dropdown>().Show();
                }
            }
            else
            {
                Vector3 refl = collisionInfo.contacts[0].normal;
                transform.forward = Vector3.Reflect((transform.position - oldpos).normalized, refl);
                print(collisionInfo.collider.gameObject.name);
            }
        }
        else
        {
            Physics.IgnoreCollision(collisionInfo.collider, this.GetComponent<Collider>());
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Dropdown")
        {
            GameObject dropdown = other.gameObject;
            if (dropdown.transform.childCount != 3)
            {
                dropdown.GetComponent<Dropdown>().Hide();
            }
            else
            {
                dropdown.GetComponent<Dropdown>().Show();
            }
        }
    }
}
