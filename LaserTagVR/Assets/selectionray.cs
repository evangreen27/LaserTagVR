using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class selectionray : MonoBehaviour
{

    [System.Serializable]
    public class Callback : UnityEvent<Ray, RaycastHit> { }

    public GameObject M4;
    public GameObject AK;
    public GameObject UMP;
    public GameObject PISTOL;
    public GameObject menucanvas;
    public GameObject shotprefab;
    public GameObject control;

    public Material lasercolor;
    public GameObject hm;

    //public Color origColor = black;

    public Transform leftHandAnchor = null;
    public Transform rightHandAnchor = null;
    public Transform centerEyeAnchor = null;
    public LineRenderer lineRenderer = null;
    public GameObject DeskPrefab;
    public GameObject ChairPrefab;
    public GameObject LockerPrefab;
    public GameObject StoragePrefab;
    public GameObject TVPrefab;
    public float maxRayDistance = 500.0f;
    public LayerMask excludeLayers;
    public selectionray.Callback raycastHitCallback;
    GameObject selectedObj = null;
    float startingz = 0.0f;
    Quaternion startingrot;

    void Awake()
    {
        if (leftHandAnchor == null)
        {
            Debug.LogWarning("Assign LeftHandAnchor in the inspector!");
            GameObject left = GameObject.Find("LeftHandAnchor");
            if (left != null)
            {
                leftHandAnchor = left.transform;
            }
        }
        if (rightHandAnchor == null)
        {
            Debug.LogWarning("Assign RightHandAnchor in the inspector!");
            GameObject right = GameObject.Find("RightHandAnchor");
            if (right != null)
            {
                rightHandAnchor = right.transform;
            }
        }
        if (centerEyeAnchor == null)
        {
            Debug.LogWarning("Assign CenterEyeAnchor in the inspector!");
            GameObject center = GameObject.Find("CenterEyeAnchor");
            if (center != null)
            {
                centerEyeAnchor = center.transform;
            }
        }
        if (lineRenderer == null)
        {
            Debug.LogWarning("Assign a line renderer in the inspector!");
            lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            lineRenderer.receiveShadows = false;
            lineRenderer.widthMultiplier = 0.02f;
            lineRenderer.tag = "ignore";
        }
    }

    Transform Pointer
    {
        get
        {
            OVRInput.Controller controller = OVRInput.GetConnectedControllers();

            if ((controller != OVRInput.Controller.None) && OVRInput.Controller.RTrackedRemote != OVRInput.Controller.None)
            {
              //  print("return right");
                //right now it will always return the right hand tracker, it wont raycast from both hands but thats fine
                return rightHandAnchor;
            }

            else if ((controller != OVRInput.Controller.None) && OVRInput.Controller.LTrackedRemote != OVRInput.Controller.None)
            {
               // print("return left");
                return leftHandAnchor;
            }

            //print("return eyetracker");
            // If no controllers are connected, we use ray from the view camera. 
            // This looks super ackward! Should probably fall back to a simple reticle!
            return centerEyeAnchor;
        }
    }

    private void Start()
    {
        control = GameObject.Find("ControlObjects");
    }

    void Update()
    {
        Transform pointer = Pointer;
        if (pointer == null)
        {
            return;
        }

        Ray laserPointer = new Ray(pointer.position, pointer.forward);

        if (lineRenderer != null)
        {
            lineRenderer.SetPosition(0, laserPointer.origin);
            lineRenderer.SetPosition(1, laserPointer.origin + laserPointer.direction * maxRayDistance);
        }


        RaycastHit hit;
        if (Physics.Raycast(laserPointer, out hit, maxRayDistance, ~excludeLayers))
        {
            if (lineRenderer != null)
            {
                lineRenderer.SetPosition(1, hit.point);
            }

            if (raycastHitCallback != null)
            {
                raycastHitCallback.Invoke(laserPointer, hit);
            }
            // hit.collider.gameObject.SendMessage("OnRaycastReceived");
            //print(hit.collider.gameObject.transform.name);
            //check if the trigger is held, if so then move the object to the xy position of the ray?
        }
        //if (OVRInput.GetUp(OVRInput.RawButton.LHandTrigger))
        if (menucanvas.activeSelf)
        {
            if (hit.collider.gameObject.CompareTag("button"))
            {
                for (int i = 0; i < GameObject.Find("Keypad").transform.childCount; i++)
                {
                    GameObject.Find("Keypad").transform.GetChild(i).GetComponent<Button>().image.color = Color.black;
                }
                hit.collider.gameObject.GetComponent<Button>().image.color = Color.gray;
                GameObject other = hit.collider.gameObject;

                if (OVRInput.GetDown(OVRInput.RawButton.RHandTrigger) || Input.GetMouseButton(0))
                {
                    if (other.gameObject.name == "OneBtn")
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

                }
            }
            else if (hit.collider.gameObject.CompareTag("input"))
            {

                if (OVRInput.GetDown(OVRInput.RawButton.RHandTrigger) || Input.GetMouseButton(0))
                {

                    GameObject input = hit.collider.gameObject;
                    input.gameObject.GetComponent<UnityEngine.UI.InputField>().Select();
                    control.GetComponent<SpawnTargets>().selected = input;
                }

                GameObject.Find("SpawnRate").GetComponent<InputField>().image.color = Color.black;
                GameObject.Find("TargetSize").GetComponent<InputField>().image.color = Color.black;
                hit.collider.gameObject.GetComponent<InputField>().image.color = Color.gray;
            }

            else if (hit.collider.gameObject.CompareTag("dropdown"))
            {

                if (OVRInput.GetDown(OVRInput.RawButton.RHandTrigger) || Input.GetMouseButton(0))
                {
                    GameObject dropdown = hit.collider.gameObject;
                    if (dropdown.transform.childCount != 3)
                    {
                        dropdown.GetComponent<Dropdown>().Hide();
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

                        ColorBlock cb = dropdown.transform.Find("Dropdown List/Viewport/Content/Item 0: Blue").GetComponent<Toggle>().colors;
                        cb.highlightedColor = Color.blue;
                        cb.normalColor = Color.blue;
                        dropdown.transform.Find("Dropdown List/Viewport/Content/Item 0: Blue").GetComponent<Toggle>().colors = cb;
                        cb = dropdown.transform.Find("Dropdown List/Viewport/Content/Item 1: Red").GetComponent<Toggle>().colors;
                        cb.normalColor = Color.red;
                        cb.highlightedColor = Color.red;
                        dropdown.transform.Find("Dropdown List/Viewport/Content/Item 1: Red").GetComponent<Toggle>().colors = cb;
                        cb = dropdown.transform.Find("Dropdown List/Viewport/Content/Item 2: Purple").GetComponent<Toggle>().colors;
                        cb.normalColor = Color.magenta;
                        cb.highlightedColor = Color.magenta;
                        dropdown.transform.Find("Dropdown List/Viewport/Content/Item 2: Purple").GetComponent<Toggle>().colors = cb;
                    }
                }
            }
            else if (hit.collider.gameObject.name == "Item 0: Blue")
            {
                if (OVRInput.GetDown(OVRInput.RawButton.RHandTrigger) || Input.GetMouseButton(0))
                {
                    lasercolor.color = Color.blue;
                    GameObject.Find("Labelcolo").GetComponent<Text>().text = "Blue";
                }
                //hit.collider.gameObject.GetComponent<>
            }
            else if (hit.collider.gameObject.name == "Item 1: Red")
            {
                if (OVRInput.GetDown(OVRInput.RawButton.RHandTrigger) || Input.GetMouseButton(0))
                {
                    lasercolor.color = Color.red;
                    GameObject.Find("Labelcolo").GetComponent<Text>().text = "Red";
                }
            }
            else if (hit.collider.gameObject.name == "Item 2: Purple")
            {
                if (OVRInput.GetDown(OVRInput.RawButton.RHandTrigger) || Input.GetMouseButton(0))
                {
                    lasercolor.color = Color.magenta;
                    GameObject.Find("Labelcolo").GetComponent<Text>().text = "Magenta";
                }
            }
            else
            {
                for (int i = 0; i < GameObject.Find("Keypad").transform.childCount; i++)
                {
                    GameObject.Find("Keypad").transform.GetChild(i).GetComponent<Button>().image.color = Color.black;
                }
                GameObject.Find("SpawnRate").GetComponent<InputField>().image.color = Color.black;
                GameObject.Find("TargetSize").GetComponent<InputField>().image.color = Color.black;
            }
        }
            



        if (OVRInput.GetDown(OVRInput.RawButton.LHandTrigger))
        {
            //when first pressing, select an object
            if (menucanvas.activeSelf)
            {
                menucanvas.SetActive(false);
                
            }
            else
            {
                menucanvas.SetActive(true);
            }
        }
        if (OVRInput.GetDown(OVRInput.RawButton.RHandTrigger) || Input.GetMouseButton(0))
        {
            //when first pressing, select an object
            

            if (hit.collider.gameObject.name.Equals("Terrain"))
                return;


            if (hit.collider.gameObject.CompareTag("gun"))
            {
                string name = hit.collider.gameObject.name;
                print(name);
                if (name.Contains("M4")) {
                    Destroy(hit.collider.gameObject);
                    M4.SetActive(true);
                    AK.SetActive(false);
                    UMP.SetActive(false);
                    PISTOL.SetActive(false);
                    rightHandAnchor.GetComponent<AudioSource>().Play();
                    shotprefab.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
                    shotprefab.GetComponent<ShotBehavior>().speed = 40f;
                }

                else if (name.Contains("Ak-47"))
                {
                    print("ak");
                    Destroy(hit.collider.gameObject);
                    M4.SetActive(false);
                    AK.SetActive(true);
                    UMP.SetActive(false);
                    PISTOL.SetActive(false);
                    leftHandAnchor.GetComponent<AudioSource>().Play();
                    shotprefab.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
                    shotprefab.GetComponent<ShotBehavior>().speed = 40f;
                }

                else if (name.Contains("UMP"))
                {
                    Destroy(hit.collider.gameObject);
                    M4.SetActive(false);
                    AK.SetActive(false);
                    UMP.SetActive(true);
                    PISTOL.SetActive(false);
                    leftHandAnchor.GetComponent<AudioSource>().Play();
                    shotprefab.transform.localScale = new Vector3(0.005f, 0.005f, 0.005f);
                    shotprefab.GetComponent<ShotBehavior>().speed = 60f;
                }

                else if (name.Contains("Pistol"))
                {
                    Destroy(hit.collider.gameObject);
                    M4.SetActive(false);
                    AK.SetActive(false);
                    UMP.SetActive(false);
                    PISTOL.SetActive(true);
                    leftHandAnchor.GetComponent<AudioSource>().Play();
                    shotprefab.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
                    shotprefab.GetComponent<ShotBehavior>().speed = 20f;
                }

                return;
            }

            return;
            selectedObj = hit.collider.gameObject;
            startingz = Vector3.Distance(this.transform.position, hit.collider.gameObject.transform.position);
            startingrot = hit.collider.gameObject.transform.rotation;


            if (selectedObj.transform.Find("BtnChair"))
            {
                GameObject chair = (GameObject)Instantiate(ChairPrefab, laserPointer.GetPoint(3), selectedObj.transform.rotation);
                selectedObj = chair;
            }
            else if (selectedObj.transform.Find("BtnDesk"))
            {
                GameObject desk = (GameObject)Instantiate(DeskPrefab, laserPointer.GetPoint(3), selectedObj.transform.rotation);
                selectedObj = desk;
            }
            else if (selectedObj.transform.Find("BtnLocker"))
            {
                GameObject locker = (GameObject)Instantiate(LockerPrefab, laserPointer.GetPoint(3), selectedObj.transform.rotation);
                selectedObj = locker;
            }
            else if (selectedObj.transform.Find("BtnStorage"))
            {
                GameObject storage = (GameObject)Instantiate(StoragePrefab, laserPointer.GetPoint(3), selectedObj.transform.rotation);
                selectedObj = storage;
            }
            else if (selectedObj.transform.Find("BtnTV"))
            {
                GameObject TV = (GameObject)Instantiate(TVPrefab, laserPointer.GetPoint(3), selectedObj.transform.rotation);
                selectedObj = TV;
            }
        }
        if (OVRInput.GetUp(OVRInput.RawButton.RHandTrigger))
        {
            //when first pressing, select an object
            print("trigger released");
            selectedObj = null;
        }
        if (selectedObj != null)
        {
            //move object
            //need zvalue distance from camera
            if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickUp))
            {
                startingz += 0.1f;
            }
            else if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickDown))
            {
                startingz -= 0.1f;
            }
            else if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickRight))
            {
                selectedObj.transform.Rotate(100* (new Vector3(0,0,1)) * Time.deltaTime, Space.Self);
                startingrot = selectedObj.transform.rotation;
            }
            else if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickLeft))
            {
                selectedObj.transform.Rotate(-100 * (new Vector3(0, 0, 1)) * Time.deltaTime, Space.Self);
                startingrot = selectedObj.transform.rotation;
            }
            selectedObj.transform.SetPositionAndRotation(laserPointer.GetPoint(startingz), startingrot);
        }
    }

    private void OnApplicationQuit()
    {
        lasercolor.color = Color.blue;
        shotprefab.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
        shotprefab.GetComponent<ShotBehavior>().speed = 20f;
    }


    IEnumerator Hitmarker()
    {
        hm.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        hm.SetActive(false);
    }
}