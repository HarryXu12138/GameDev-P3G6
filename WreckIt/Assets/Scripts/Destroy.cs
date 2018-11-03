using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Destroy : MonoBehaviour {

    public GameObject debriPrefab;
    private GameObject bb;
    public int prefabNumbers;
    public float prefabDestroyTime;
    public float spread;
    public float shift;
    public float impulseMagnitude = 1;
    
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
            //GameObject temp = GameObject.Instantiate<GameObject>(debriPrefab);
            //temp.transform.position = transform.position;
            Vector3 force = collision.impulse;
            force.Scale(new Vector3(-1f*impulseMagnitude, -1f * impulseMagnitude, -1f * impulseMagnitude));
            //force = Random.onUnitSphere * collision.impulse.magnitude;
            createDebris(force);
            
            print("Bulls Eyes");
            print(force);
        }
    }

    private void addForceToPrefab(GameObject prefab, Vector3 force)
    {
        Rigidbody rb = prefab.GetComponent<Rigidbody>();
        rb.AddForce(force, ForceMode.Impulse);
    }

    private void createDebris(Vector3 force)
    {
        int counter = prefabNumbers;
        /*
        int offset = 0;
        while((offset*2+1)*(offset*2+1) < counter)
        {
            offset += 1;
        }
        Vector3 startPoint = transform.position;
        startPoint.x += offset;
        startPoint.y += offset;
        startPoint.z += offset;
        */
        while (counter > 0)
        {
            GameObject temp = GameObject.Instantiate<GameObject>(debriPrefab);
            temp.transform.position = transform.position;
            //check the direction
            Vector3 tempF = force;
            print(Mathf.Abs(tempF.x - 0f) < 0.0001f);
            if (Mathf.Abs(tempF.x - 0f) < 0.0001f)
            {
                //print("work");
                tempF.x += Random.Range(-spread + shift, spread + shift);
            }
            if (Mathf.Abs(tempF.y - 0f) < 0.0001f)
            {
                tempF.y += Random.Range(-spread + shift, spread + shift);
            }
            if (Mathf.Abs(tempF.z - 0f) < 0.0001f)
            {
                tempF.z += Random.Range(-spread + shift, spread + shift);
            }
            print(tempF);
            addForceToPrefab(temp, tempF);
            Destroy(temp, prefabDestroyTime);
            counter--;
            
        }
    }




}
