using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gledar : MonoBehaviour
{
    float startLine = 0f;
    float endLine = 0f;

    Renderer rend;

    void Start()
    {
        rend = this.GetComponent<Renderer>();
    }

    void Update()
    {
        if (endLine < 1f)
        {
            endLine += 6 * Time.deltaTime;
        }

        if (endLine > 0.9f && startLine < 1f)
        {
            startLine += 6 * Time.deltaTime;
        }

        rend.material.SetFloat("_ShapeEnd", endLine);
        rend.material.SetFloat("_ShapeStart", startLine);
    }
}
