using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using 

public class PlayerController : MonoBehaviour
{
    public GameObject cameraPivot;

    public Animator anim;


    public float hp;

    public float maxHp;

    public CharacterStatus characterStatus;
    public InputController inputController;
    public float vertical, horizontal, moveAmount;
    public Transform cameraTransform;
    //Transform modelTransform;

    public Vector3 rotationDirection;
    public Vector3 moveDirection;

    public float rotationSpeed;

    Transform plTransform;

    Rigidbody playerRigidbody;
    CapsuleCollider capsuleCollider;


    GameObject car;
 

    public float speed = 0.1f;

    public float mouseSemouseSensitivity = 1;

    Vector3 SpawnPos = new Vector3(0, 0, 0);




    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        plTransform = GetComponent<Transform>();
        GetOutCar(SpawnPos);
        
    }

    public void Die() {
        if (characterStatus.isInCar == true) {
            GetOutCar(characterStatus.Car.GetComponent<CarSit>().exitTransform.position);
        }

        capsuleCollider.height = 1.5f;
        capsuleCollider.radius = 0.2f;
        capsuleCollider.direction = 2;
        capsuleCollider.center = new Vector3(capsuleCollider.center.x, 0.35f, capsuleCollider.center.z);

        anim.SetBool("die", true);
    }

    public void MoveUpdate() {
        if (characterStatus.isAlive == true)
        {
            vertical = inputController.vAxis;
            horizontal = inputController.hAxis;
            moveAmount = Mathf.Clamp01(Mathf.Abs(vertical) + Mathf.Abs(horizontal));



            if (characterStatus.isInCar == false)
            {

                anim.SetFloat("vertical", moveAmount, 0.15f, Time.deltaTime);


                Vector3 moveDir = cameraTransform.forward * vertical;
                moveDir += cameraTransform.right * horizontal;


                //playerTransform.Translate(moveDir * speed * moveAmount);

                transform.Translate(0, 0, speed * moveAmount);

                moveDir.Normalize();
                moveDirection = moveDir;



                rotationDirection = cameraTransform.forward;
                RotationNoramal();
            }
            else
            {
                transform.position = characterStatus.Car.transform.position;

            }


        }
        else {
            moveAmount = 0;
        }
        
    }

    public void SittingCar() {
        characterStatus.isInCar = true;
        playerRigidbody.isKinematic = true;
        capsuleCollider.enabled = false;
        playerRigidbody.useGravity = false;
        anim.SetBool("incar", true);
    }

    public void GetOutCar(Vector3 exit) {
        characterStatus.isInCar = false;
        playerRigidbody.isKinematic = false;
        capsuleCollider.enabled = true;
        playerRigidbody.useGravity = true;
        anim.SetBool("incar", false);
        if (exit != null)
        {
            transform.position = exit;
        }
    }


    public void RotationNoramal() {
        if (!characterStatus.isAiming)
        {
            rotationDirection = moveDirection;
        }

        Vector3 targetDir = rotationDirection;
        targetDir.y = 0;

        Quaternion lookDir = Quaternion.LookRotation(targetDir);
        Quaternion targetRot = Quaternion.Slerp(transform.rotation, lookDir, rotationSpeed);
        transform.rotation = targetRot;
    
    }




    /*double ModelToTargetAngle(float angle,float target) {
        if (target > angle + rotationSpeed)
        {
            angle += rotationSpeed;
        }
        else if (target < angle - rotationSpeed)
        {
            angle -= rotationSpeed;
        }
        else {
            angle = target;
        }
        return angle;
    
    }
    */

    // Update is called once per frame
   /* void FixedUpdate()
    {
        vAxis = inputController.vAxis;
        hAxis = inputController.hAxis;
        xMouse = inputController.xMouse;
        yMouse = inputController.yMouse;

        X +=  Input.GetAxis("Mouse X") * mouseSemouseSensitivity;
        Y += Input.GetAxis("Mouse Y") * mouseSemouseSensitivity;
        Y = Mathf.Clamp(Y, -yLimit, yLimit);
        
        transform.eulerAngles = new Vector3(0, X, 0);
        pivotTransform.localEulerAngles = new Vector3(Y, 0, 0);

        if (hAxis != 0 || vAxis != 0)
        {
            angleMove = Math.Atan(hAxis / vAxis) * 57.32;
        }

        if (vAxis < 0) {
            angleMove = angleMove+180;
        }

        if (hAxis > 0.2 || vAxis > 0.2)
        {
            modelTransform.rotation = Quaternion.Euler(0, (float)angleMove + X, 0);
            //tempAngle = modelTransform.rotation.y;
        }
        else {
            modelTransform.localEulerAngles = new Vector3(0, (float)angleMove, 0);
        }
        //YYYY = modelTransform.rotation.y*180;
        //TAREGET = (float)angleMove + X;
        //modelTransform.rotation = Quaternion.Euler(0, (float)ModelToTargetAngle(modelTransform.rotation.y*180, (float)angleMove + X), 0);
        //modelTransform.localEulerAngles = new Vector3(0, ModelToTargetAngle(modelTransform.localRotation.y, (float)angleMove/180), 0);

        
         transform.Translate(new Vector3(-hAxis, 0, -vAxis)*speed);



    }*/
}
