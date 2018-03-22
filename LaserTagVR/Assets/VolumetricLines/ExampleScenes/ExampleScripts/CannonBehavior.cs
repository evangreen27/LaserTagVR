using UnityEngine;
using System.Collections;

public class CannonBehavior : MonoBehaviour {

	public Transform m_cannonRot;
	public Transform m_muzzle;
	public GameObject m_shotPrefab;
    public GameObject m_telePrefab;
    public Texture2D m_guiTexture;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
		{
			GameObject go = GameObject.Instantiate(m_shotPrefab, m_muzzle.position, m_muzzle.rotation) as GameObject;
			GameObject.Destroy(go, 10f);
		}

        if (OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger))
        {
            GameObject ge = GameObject.Instantiate(m_telePrefab, m_muzzle.position, m_muzzle.rotation) as GameObject;
            GameObject.Destroy(ge, 10f);
        }
    }

}
