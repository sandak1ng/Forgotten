using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed;
	public GameObject PlayerObject;

	// Use this for initialization
	void Start () {
		Vector3 savedPosition = new Vector3 (PlayerPrefs.GetFloat ("playerX"), PlayerPrefs.GetFloat ("playerY"), PlayerPrefs.GetFloat ("playerZ"));
		PlayerObject.transform.position = savedPosition;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxisRaw ("Horizontal") > 0.5f || Input.GetAxisRaw ("Horizontal") < -0.5f) 
		{
			transform.Translate (new Vector3 (Input.GetAxisRaw ("Horizontal") * speed * Time.deltaTime, 0f, 0f));
		}

		if (Input.GetAxisRaw ("Vertical") > 0.5f || Input.GetAxisRaw ("Vertical") < -0.5f) 
		{
			transform.Translate (new Vector3 (0f, Input.GetAxisRaw ("Vertical") * speed * Time.deltaTime, 0f));
		}

		PlayerPrefs.SetFloat ("playerX", PlayerObject.transform.position.x);
		PlayerPrefs.SetFloat ("playerY", PlayerObject.transform.position.y);
		PlayerPrefs.SetFloat ("playerZ", PlayerObject.transform.position.z);
	}
}
