using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour
{
    public Material yellow;
    public Material white;

    Renderer rend2;
    //Mesh mesh;

    void Start()
    {
        rend2 = this.GetComponent<Renderer>();
        //mesh = this.GetComponent<Mesh>();

    }

    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger))//OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger)
        {
            Debug.Log("ON");
            rend2.material = yellow;
            rend2.material.SetFloat("_AutoTime", 1);
            //mesh.RecalculateBounds();
        }
        else if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            Debug.Log("OFF");
            rend2.material.SetFloat("_AutoTime", 0);
            rend2.material = white;
            //mesh.RecalculateBounds();
        }
    }
}
