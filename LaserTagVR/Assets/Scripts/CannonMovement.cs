using UnityEngine;
using System.Collections;

public class CannonMovement : MonoBehaviour
{
    public Transform teleprefab;
    void OnJoinedRoom()
    {
        CreatePlayerObject();
    }

    void CreatePlayerObject()
    {
        
       // teleprefab.GetComponent<TeleBehavior>().player = newPlayerObject;
    }
}
