using UnityEngine;
using System.Collections;

public class ShakeyShakey : MonoBehaviour
{
    [SerializeField]
    Vector3 originPosition;

    public float shake_speed;
    public float shake_intensity;

    public bool isShaking = true;

    void Start()
    {
        originPosition = transform.position;
    }

    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            isShaking = false;
        } else if (OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger))
        {
            isShaking = true;
        }

        if (isShaking)
        {
            float step = shake_speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, originPosition + Random.insideUnitSphere, step);
        }
    }
}
