using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float distance = 5.0f;
    public float height = 2.0f;
    public float rotationSpeed = 5.0f;
    public float verticalAngleLimit = 60.0f;

    private float currentRotationAngle = 0.0f;
    private float currentVerticalAngle = 0.0f;

    private void LateUpdate()
    {
        if (target != null)
        {
            currentRotationAngle += CrossPlatformInputManager.GetAxis("Mouse X") * rotationSpeed;
            currentVerticalAngle -= CrossPlatformInputManager.GetAxis("Mouse Y") * rotationSpeed;
            currentVerticalAngle = Mathf.Clamp(currentVerticalAngle, -verticalAngleLimit, verticalAngleLimit);

            Quaternion currentRotation = Quaternion.Euler(currentVerticalAngle, currentRotationAngle, 0.0f);

            Vector3 targetPosition = target.position - currentRotation * Vector3.forward * distance;
            targetPosition.y += height;

            transform.position = targetPosition;
            transform.LookAt(target.position + Vector3.up * height);
        }
    }
}
