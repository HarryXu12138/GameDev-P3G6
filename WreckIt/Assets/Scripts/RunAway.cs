using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAway : MonoBehaviour {
    public Transform ex;
    public float check;
    public Rigidbody rb;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 vec = new Vector3(1, (Mathf.Abs(Mathf.Sin(3.5f*Time.time))+1)/2, 1);
        check = (Mathf.Abs(Mathf.Sin(3.5f * Time.time)) + 1) / 2;
        ex.transform.localScale = vec;
    }
}
