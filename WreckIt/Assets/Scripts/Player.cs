using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float MaxSpeed = 3.0f;
    public Vector3 CurrentSpeed = new Vector3(0f, 0f, 0f);
    public float Acceleration = 0.3f;

    private GameObject cam;

	// Use this for initialization
	void Start () {
        cam = GameObject.Find("Main Camera");
	}

	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.W)) {
            if (CurrentSpeed.magnitude < MaxSpeed)
            {
            }
        }
        cam.transform.position = transform.position;
	}
}
