using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraOrbit : MonoBehaviour
{
    public GameObject OrbitTarget;
    public float OrbitSpeed = 1f;

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = transform.localPosition + transform.right * Input.GetAxis("Horizontal") * OrbitSpeed;
            
        transform.LookAt(OrbitTarget.transform, Vector3.up);
    }
}
