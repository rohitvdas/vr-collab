using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasFollowCamera : MonoBehaviour {

    public Camera cameraToLookAt;

    public void SetCam(Camera cam)
    {
        this.cameraToLookAt = cam;
    }

    void Start()
    {
        //transform.Rotate( 180,0,0 );
    }

    void Update()
    {
        if (this.cameraToLookAt == null)
        {
            return;
        }
        Vector3 v = cameraToLookAt.transform.position - transform.position;
        v.x = v.z = 0.0f;
        transform.LookAt(cameraToLookAt.transform.position - v);
        transform.position = cameraToLookAt.transform.position + Vector3.forward * 10.0f;
        transform.Rotate(0, 180, 0);
        //transform.rotation = cameraToLookAt.transform.rotation;
    }

}
