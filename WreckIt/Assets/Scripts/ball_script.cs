using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball_script : MonoBehaviour {

    private float speedX = 2.0f;
    private float speedY = 2.0f;

    private float angleX = 0.0f;
    private float angleY = 0.0f;

    private GameObject da_cam;
    private Rigidbody rb;

    // Use this for initialization
    void Start () {
        da_cam = GameObject.Find("Main Camera");
        transform.position = da_cam.transform.position + (new Vector3(0.5f, 0.0f, 1.0f));
        transform.SetParent(da_cam.transform);
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Mouse0)){
            transform.SetParent(null);
            //throw the ball upon mouse click
            rb.useGravity = true;
            Vector3 velocity = new Vector3(0, 0, 2.0f);

            //kinda odd that I can't use *= on this
            velocity = Quaternion.Euler(da_cam.transform.rotation.x, da_cam.transform.rotation.y, 0) * velocity;

            rb.AddRelativeForce(velocity);
            
        }


    }
}
