using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Func<Vector3> GetCameraFollowPosiotionFunc;

    public void Setup(Func<Vector3> cameraFollowPosition)
    {
        this.GetCameraFollowPosiotionFunc = cameraFollowPosition;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraFollowPosition = GetCameraFollowPosiotionFunc();
        cameraFollowPosition.z = transform.position.z;
        transform.position = cameraFollowPosition;
    }
}
