﻿using UnityEngine;
using UnityEngine.Events;

public class selectionray : MonoBehaviour
{

    [System.Serializable]
    public class Callback : UnityEvent<Ray, RaycastHit> { }

    public GameObject M4;
    public GameObject AK;
    public GameObject UMP;
    public GameObject PISTOL;

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
            lineRenderer.tag = "raycast";
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
                }

                else if (name.Contains("Ak"))
                {
                    Destroy(hit.collider.gameObject);
                    M4.SetActive(false);
                    AK.SetActive(true);
                    UMP.SetActive(false);
                    PISTOL.SetActive(false);
                }

                else if (name.Contains("UMP"))
                {
                    Destroy(hit.collider.gameObject);
                    M4.SetActive(false);
                    AK.SetActive(false);
                    UMP.SetActive(true);
                    PISTOL.SetActive(false);
                }

                else if (name.Contains("Pistol"))
                {
                    Destroy(hit.collider.gameObject);
                    M4.SetActive(false);
                    AK.SetActive(false);
                    UMP.SetActive(false);
                    PISTOL.SetActive(true);
                }

                return;
            }


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
}