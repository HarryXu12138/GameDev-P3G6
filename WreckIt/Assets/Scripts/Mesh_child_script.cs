using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mesh_child_script : MonoBehaviour
{
    public GameObject parent;
    // Use this for initialization
    void Start()
    {
        transform.SetParent(parent.transform);
    }

    public void OnCollisionEnter(Collision collision)
    {
        transform.SetParent(null);
        parent.SendMessage("collapse", true, SendMessageOptions.DontRequireReceiver);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
