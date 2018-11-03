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
    public float impulseMagnitude = 1.0f;
    public float breakpoint;
    private bool trigger = false;
    // Use this for initialization
    void Start(){
        bb = GameObject.Find("da_ball");
        Assert.IsNotNull(bb);
        
    }

    private void changeTrigger(bool i){
        trigger = i;    
    }

	// Update is called once per frame
	void Update () {
		
	}

   

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject == bb && trigger)
        {


            //GameObject temp = GameObject.Instantiate<GameObject>(debriPrefab);
            //temp.transform.position = transform.position;
            Vector3 force = collision.impulse;
            force.Scale(new Vector3(-1f*impulseMagnitude, -1f * impulseMagnitude, -1f * impulseMagnitude));
            //force = Random.onUnitSphere * collision.impulse.magnitude;
            print(force);
            if (Mathf.Abs(force.x + force.y + force.z) > breakpoint)
            {
                createDebris(force);
                GameObject.Destroy(gameObject);      
                print("Bulls Eyes");

            }
            
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
            //print(Mathf.Abs(tempF.x - 0f) < 0.01f);
            if (Mathf.Abs(tempF.x - 0f) < 0.03f)
            {
                //print("work");
                tempF.x += Random.Range(-spread + shift, spread + shift);
            }
            if (Mathf.Abs(tempF.y - 0f) < 0.03f)
            {
                tempF.y += Random.Range(-spread + shift, spread + shift);
                if (tempF.y<0){
                    tempF.y = -tempF.y;
                    if (tempF.y < 2.0f){
                        tempF.y = 2.0f;
                    } 
                }
            }
            if (Mathf.Abs(tempF.z - 0f) < 0.03f)
            {
                tempF.z += Random.Range(-spread + shift, spread + shift);
            }
            //print(tempF);
            addForceToPrefab(temp, tempF);
            Destroy(temp, prefabDestroyTime);
            counter--;
            
        }
    }




}
