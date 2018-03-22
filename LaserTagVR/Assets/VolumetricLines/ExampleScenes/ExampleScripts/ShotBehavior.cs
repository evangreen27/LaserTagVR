using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShotBehavior : MonoBehaviour {
    GameObject selected;
    Vector3 oldpos;
    public GameObject control;
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
        else if (other.gameObject.name == "TargetSize" || other.gameObject.name == "SpawnRate")
        {
            GameObject input = other.gameObject;
            selected = input.transform.GetChild(1).gameObject;
            selected.GetComponent<Text>().text = "";
        }
        else if (other.gameObject.name == "OneBtn")
        {
            selected.GetComponent<Text>().text += "1";
        }
        else if (other.gameObject.name == "TwoBtn")
        {
            selected.GetComponent<Text>().text += "2";
        }
        else if (other.gameObject.name == "ThreeBtn")
        {
            selected.GetComponent<Text>().text += "3";
        }
        else if (other.gameObject.name == "FourBtn")
        {
            selected.GetComponent<Text>().text += "4";
        }
        else if (other.gameObject.name == "FiveBtn")
        {
            selected.GetComponent<Text>().text += "5";
        }
        else if (other.gameObject.name == "SixBtn")
        {
            selected.GetComponent<Text>().text += "6";
        }
        else if (other.gameObject.name == "SevenBtn")
        {
            selected.GetComponent<Text>().text += "7";
        }
        else if (other.gameObject.name == "EightBtn")
        {
            selected.GetComponent<Text>().text += "8";
        }
        else if (other.gameObject.name == "NineBtn")
        {
            selected.GetComponent<Text>().text += "9";
        }
        else if (other.gameObject.name == "ZeroBtn")
        {
            selected.GetComponent<Text>().text += "0";
        }
        else if (other.gameObject.name == "ClearBtn")
        {
            selected.GetComponent<Text>().text = "";
        }
        else if (other.gameObject.name == "DotBtn")
        {
            selected.GetComponent<Text>().text += ".";
        }
        else if (other.gameObject.name == "EnterBtn")
        {
            if(selected.transform.parent.gameObject.name == "SpawnRate")
                control.GetComponent<SpawnTargets>().spawnrate = float.Parse(selected.GetComponent<Text>().text);
            else if(selected.transform.parent.gameObject.name == "TargetSize")
            {
                control.GetComponent<SpawnTargets>().targetSize = float.Parse(selected.GetComponent<Text>().text);
            }
        }
    }
}
