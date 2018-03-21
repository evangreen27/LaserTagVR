using UnityEngine;
using System.Collections;

public class CannonMovement : MonoBehaviour
{
    public RPGCamera Camera;
    public Transform teleprefab;
    void OnJoinedRoom()
    {
        CreatePlayerObject();
    }

    void CreatePlayerObject()
    {
        Vector3 position = new Vector3(-1.09f, 8.39f, -19.12f);

        GameObject newPlayerObject = PhotonNetwork.Instantiate("OVRCameraRig", position, Quaternion.identity, 0);

        Camera.Target = newPlayerObject.transform;
       // teleprefab.GetComponent<TeleBehavior>().player = newPlayerObject;
    }
}
