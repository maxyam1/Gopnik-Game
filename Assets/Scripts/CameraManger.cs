using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CameraManger : MonoBehaviour
{
    public InputController inputController; 

    public Transform camTrans;
    public Transform pivot;
    public Transform Character;
    public Transform mTransform;

    public Transform targetLook;

    public CharacterStatus characterStatus;
    public CameraConfig cameraConfig;
    public bool leftPivot;
    public float delta;

    public float mouseX;
    public float mouseY;
    public float smoothX;
    public float smoothY;
    public float smoothXVelocity;
    public float smoothYVelocity;
    public float lookAngle;
    public float titlAngle;


    private void Start()
    {
        SetCameraAtPlayer();
        if (!inputController) {
            inputController = GetComponent<InputController>();
        }
        if (!cameraConfig) {
            cameraConfig = GetComponent<PropertiesHolder>().cameraConfig;
        }
        if (!characterStatus) {
            characterStatus = GetComponent<PropertiesHolder>().characterStatus;
        }
    }
    private void Update()
    {
        Tick();
    }

    /*public void ChangeCameraPivot(GameObject newPivot) {
        pivot = newPivot.GetComponent<Transform>();
    
    }*/


    public void SetCameraAtPlayer() {
        cameraConfig.aimX = 0.4f;
            cameraConfig.aimZ = -1f;
        cameraConfig.maxAngle = 50;
        cameraConfig.minAngle = -35;
        cameraConfig.normalX = 0.4f;
        cameraConfig.normalY = 1.5f;
        cameraConfig.normalZ = -1.72f;
        cameraConfig.pivotSpeed = 9f;
        cameraConfig.turnSmooth = 0.1f;
        cameraConfig.X_rot_speed = 7f;
        cameraConfig.Y_rot_speed = 7f;



    }
    /*
    public void SetCameraAtVaz()
    {
        cameraConfig.aimX = 0.4f;
        cameraConfig.aimZ = -1f;
        cameraConfig.maxAngle = 50;
        cameraConfig.minAngle = -35;
        cameraConfig.normalX = 0f;
        cameraConfig.normalY = 1.5f;
        cameraConfig.normalZ = -5f;
        cameraConfig.pivotSpeed = 9f;
        cameraConfig.turnSmooth = 0.1f;
        cameraConfig.X_rot_speed = 7f;
        cameraConfig.Y_rot_speed = 7f;



    }*/

    void Tick()
    {
        delta = Time.deltaTime;
        HandlePosition();
        HandleRotation();

        Vector3 targetPosition = Vector3.Lerp(mTransform.position, Character.position, 1);
        mTransform.position = targetPosition;
        TargetLook();
    }




    void TargetLook() {
        Ray ray = new Ray(camTrans.position, camTrans.forward * 2000);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            targetLook.position = Vector3.Lerp(targetLook.position, hit.point, Time.deltaTime * 40);
        }
        else {
            targetLook.position = Vector3.Lerp(targetLook.position, targetLook.transform.forward * 200, Time.deltaTime * 40);
        }
    }





    void HandlePosition()
    {
        float targetX = cameraConfig.normalX;
        float targetY = cameraConfig.normalY;
        float targetZ = cameraConfig.normalZ;

        if (characterStatus.isAiming) {
            targetX = cameraConfig.aimX;
            targetZ = cameraConfig.aimZ;
        }

        if (leftPivot) {
            targetX = -targetX;
        }

        Vector3 newPivotPosition = pivot.localPosition;
        newPivotPosition.x = targetX;
        newPivotPosition.y = targetY;

        Vector3 newCameraPosition = camTrans.localPosition;
        newCameraPosition.z = targetZ;

        float t = delta * cameraConfig.pivotSpeed;
        pivot.localPosition = Vector3.Lerp(pivot.localPosition, newPivotPosition, t);
        camTrans.localPosition = Vector3.Lerp(camTrans.localPosition, newCameraPosition, t);
    }

    void HandleRotation() 
    {
        mouseX = inputController.xMouse;
        mouseY = inputController.yMouse;
        if (cameraConfig.turnSmooth > 0)
        {
            smoothX = Mathf.SmoothDamp(smoothX, mouseX, ref smoothXVelocity, cameraConfig.turnSmooth);
            smoothY = Mathf.SmoothDamp(smoothY, mouseY, ref smoothYVelocity, cameraConfig.turnSmooth);
        }
        else {
            smoothX = mouseX;
            smoothY = mouseY;
        }

        lookAngle += smoothX * cameraConfig.X_rot_speed;
        Quaternion targetRot = Quaternion.Euler(0, lookAngle, 0);
        mTransform.rotation = targetRot;

        titlAngle -= smoothY * cameraConfig.Y_rot_speed;
        titlAngle = Mathf.Clamp(titlAngle, cameraConfig.minAngle, cameraConfig.maxAngle);
        pivot.localRotation = Quaternion.Euler(titlAngle, 0, 0);
    }

}
