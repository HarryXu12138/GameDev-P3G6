using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Destroy : MonoBehaviour {

    public GameObject debriPrefab;
    private GameObject bb;
    
    // Use this for initialization
    void Start(){
        bb = GameObject.Find("BounceBall");
        Assert.IsNotNull(bb);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject == bb)
        {
            GameObject.Destroy(gameObject);
            GameObject temp = GameObject.Instantiate<GameObject>(debriPrefab);
            temp.transform.position = transform.position;
            Vector3 force = collision.impulse;
            force.x *= -1;
            force.y = 1f;
            force.z = 0f;
            addForceToPrefab(temp, force);
            
            print("Bulls Eyes");
            print(force);
        }
    }

    private void addForceToPrefab(GameObject prefab, Vector3 force)
    {
        Rigidbody rb = prefab.GetComponent<Rigidbody>();
        rb.AddForce(force, ForceMode.Impulse);
    }
}
