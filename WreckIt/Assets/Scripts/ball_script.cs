using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball_script : MonoBehaviour {

    public float throw_speed = 2.0f;

    private float speedX = 2.0f;
    private float speedY = 2.0f;

    private float angleX = 0.0f;
    private float angleY = 0.0f;

    private GameObject da_cam;
    private Rigidbody rb;

    private bool thrown = false;
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
        if (Input.GetKey(KeyCode.E) && !thrown){
            thrown = true;
            transform.SetParent(null);
            //throw the ball upon clicking the E button
            rb.useGravity = true;
            Vector3 velocity = new Vector3(0, 0, throw_speed);

            //kinda odd that I can't use *= on this
            velocity = Quaternion.Euler(da_cam.transform.rotation.x, da_cam.transform.rotation.y, 0) * velocity;

            rb.AddRelativeForce(velocity);
            
        }


    }
}
