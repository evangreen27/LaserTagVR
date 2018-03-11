using UnityEngine;
using UnityEngine.Events;

public class selectionray : MonoBehaviour
{

    [System.Serializable]
    public class Callback : UnityEvent<Ray, RaycastHit> { }

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

            print("return eyetracker");
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
        if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
        {
            //when first pressing, select an object
            print("trigger pressed");
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
        if (OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger))
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