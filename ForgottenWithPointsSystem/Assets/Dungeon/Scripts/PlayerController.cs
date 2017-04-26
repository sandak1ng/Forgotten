using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed;
    private bool facingRight;
    private Animator anim;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        facingRight = true; 
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
        anim.SetFloat("moveX", Input.GetAxisRaw("Horizontal"));
        anim.SetFloat("moveY", Input.GetAxisRaw("Vertical"));
	}
     
}
