using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderCube : MonoBehaviour
{
    /*void Update()
    {
        ConsoleProDebug.Watch("Cube X", transform.position.x.ToString());
        ConsoleProDebug.Watch("Cube Y", transform.position.y.ToString());
        ConsoleProDebug.Watch("Cube Z", transform.position.z.ToString());
    }*/

    void OnTriggerEnter()
    {
        Debug.Log("berhasil meong");
    }
}
