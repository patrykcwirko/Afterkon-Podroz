using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Func<Vector3> GetCameraFollowPosiotionFunc;

    private float cameraMoveSpeed = 10f;
    public void Setup(Func<Vector3> cameraFollowPosition)
    {
        this.GetCameraFollowPosiotionFunc = cameraFollowPosition;
    }

    public void SetGetCameraFollowPosiotionFunc(Func<Vector3> GetCameraFollowPosiotionFunc)
    {
        this.GetCameraFollowPosiotionFunc = GetCameraFollowPosiotionFunc;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraFollowPosition = GetCameraFollowPosiotionFunc();
        cameraFollowPosition.z = transform.position.z;

        Vector3 cameraMoveDir = (cameraFollowPosition - transform.position).normalized;
        float distance = Vector3.Distance(cameraFollowPosition, transform.position);

        if (distance > 0)
        {
            Vector3 newCameraPosition = transform.position + cameraMoveDir * distance * cameraMoveSpeed * Time.deltaTime;

            float distanceAfterMoving = Vector3.Distance(newCameraPosition, cameraFollowPosition);

            if (distanceAfterMoving > distance)
            {
                newCameraPosition = cameraFollowPosition;
            }

            transform.position = newCameraPosition;
        }
    }
}
