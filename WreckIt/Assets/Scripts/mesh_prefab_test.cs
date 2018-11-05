using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mesh_prefab_test : MonoBehaviour {


    private GameObject[] children;
    public string child_tag;
    // Use this for initialization
    void Start () {
        children = GameObject.FindGameObjectsWithTag(child_tag);
        for (int a = 0; a < children.Length; a++)
        {
            children[a].GetComponent<Rigidbody>().isKinematic = true;
            children[a].GetComponent<Rigidbody>().useGravity = false;
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
    /*
    public void collapse()
    {
        for(int a = 0; a < children.Length; a++)
        {
            children[a].GetComponent<Rigidbody>().isKinematic = false;
            children[a].GetComponent<Rigidbody>().useGravity = true;
        }
    }
    */
}
