using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShotBehavior : MonoBehaviour {
    public GameObject selectede;
    public GameObject selected;
    Vector3 oldpos;
    public GameObject control;
    public Material lasercolor;
    public GameObject pop;
    public GameObject impact;
    // Use this for initialization
    void Start () {
        oldpos = transform.position;
        control = GameObject.Find("ControlObjects");

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
                GameObject g = GameObject.Instantiate(pop, collisionInfo.contacts[0].point, Quaternion.identity);
                Destroy(g, 3f);
                GameObject player = GameObject.Find("OVRCameraRig");
                player.GetComponent<AudioSource>().Play();
                GameObject scoretext = GameObject.Find("scoretext");
                float pts = GameObject.Find("ControlObjects").GetComponent<SpawnTargets>().targetsShot++;
                scoretext.GetComponent<UnityEngine.UI.Text>().text = "Score: " + pts;
                Destroy(collisionInfo.collider.gameObject);
                Destroy(this.gameObject);
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
                GameObject g = GameObject.Instantiate(impact, collisionInfo.contacts[0].point, Quaternion.identity);
                Destroy(g, 3f);
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
                //dropdown.GetComponent<Dropdown>().Hide();
            }
            else
            {
                dropdown.GetComponent<Dropdown>().Show();
                print(dropdown.transform.Find("Dropdown List"));
                BoxCollider c = dropdown.transform.Find("Dropdown List/Viewport/Content/Item 0: Blue").gameObject.AddComponent<BoxCollider>();
                c.size = new Vector3(180, 20, 10);
                c.isTrigger = true;
                BoxCollider d = dropdown.transform.Find("Dropdown List/Viewport/Content/Item 1: Red").gameObject.AddComponent<BoxCollider>();
                d.size = new Vector3(180, 20, 10);
                d.isTrigger = true;
                BoxCollider e = dropdown.transform.Find("Dropdown List/Viewport/Content/Item 2: Purple").gameObject.AddComponent<BoxCollider>();
                e.size = new Vector3(180, 20, 10);
                e.isTrigger = true;
            }
        }
        else if (other.gameObject.name == "TargetSize" || other.gameObject.name == "SpawnRate")
        {
            GameObject input = other.gameObject;
            input.gameObject.GetComponent<UnityEngine.UI.InputField>().Select();
            control.GetComponent<SpawnTargets>().selected = input;
        }
        else if (other.gameObject.name == "OneBtn")
        {
            print(control.GetComponent<SpawnTargets>().selected);
            control.GetComponent<SpawnTargets>().selected.gameObject.GetComponent<UnityEngine.UI.InputField>().text += "1";
        }
        else if (other.gameObject.name == "TwoBtn")
        {
            control.GetComponent<SpawnTargets>().selected.gameObject.GetComponent<UnityEngine.UI.InputField>().text += "2";
        }
        else if (other.gameObject.name == "ThreeBtn")
        {
            control.GetComponent<SpawnTargets>().selected.gameObject.GetComponent<UnityEngine.UI.InputField>().text += "3";
        }
        else if (other.gameObject.name == "FourBtn")
        {
            control.GetComponent<SpawnTargets>().selected.gameObject.GetComponent<UnityEngine.UI.InputField>().text += "4";
        }
        else if (other.gameObject.name == "FiveBtn")
        {
            control.GetComponent<SpawnTargets>().selected.gameObject.GetComponent<UnityEngine.UI.InputField>().text += "5";
        }
        else if (other.gameObject.name == "SixBtn")
        {
            control.GetComponent<SpawnTargets>().selected.gameObject.GetComponent<UnityEngine.UI.InputField>().text += "6";
        }
        else if (other.gameObject.name == "SevenBtn")
        {
            control.GetComponent<SpawnTargets>().selected.gameObject.GetComponent<UnityEngine.UI.InputField>().text += "7";
        }
        else if (other.gameObject.name == "EightBtn")
        {
            control.GetComponent<SpawnTargets>().selected.gameObject.GetComponent<UnityEngine.UI.InputField>().text += "8";
        }
        else if (other.gameObject.name == "NineBtn")
        {
            control.GetComponent<SpawnTargets>().selected.gameObject.GetComponent<UnityEngine.UI.InputField>().text += "9";
        }
        else if (other.gameObject.name == "ZeroBtn")
        {
            control.GetComponent<SpawnTargets>().selected.gameObject.GetComponent<UnityEngine.UI.InputField>().text += "0";
        }
        else if (other.gameObject.name == "ClearBtn")
        {
            control.GetComponent<SpawnTargets>().selected.gameObject.GetComponent<UnityEngine.UI.InputField>().text = "";
        }
        else if (other.gameObject.name == "DotBtn")
        {
            control.GetComponent<SpawnTargets>().selected.gameObject.GetComponent<UnityEngine.UI.InputField>().text += ".";
        }
        else if (other.gameObject.name == "EnterBtn")
        {
            if (control.GetComponent<SpawnTargets>().selected.gameObject.name == "SpawnRate")
            {
                print(float.Parse(control.GetComponent<SpawnTargets>().selected.gameObject.GetComponent<UnityEngine.UI.InputField>().text));
                control.GetComponent<SpawnTargets>().spawnrate = float.Parse(control.GetComponent<SpawnTargets>().selected.gameObject.GetComponent<UnityEngine.UI.InputField>().text);
            }
            else if (control.GetComponent<SpawnTargets>().selected.gameObject.name == "TargetSize")
            {
                control.GetComponent<SpawnTargets>().targetSize = int.Parse(control.GetComponent<SpawnTargets>().selected.gameObject.GetComponent<UnityEngine.UI.InputField>().text);
            }
        }
        else if (other.gameObject.name == "Item 0: Blue")
        {
            lasercolor.color = Color.blue;
        }
        else if (other.gameObject.name == "Item 1: Red")
        {
            lasercolor.color = Color.red;
        }
        else if (other.gameObject.name == "Item 2: Purple")
        {
            lasercolor.color = Color.magenta;
        }
    }


}
