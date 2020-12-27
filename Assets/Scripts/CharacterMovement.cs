using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterMovement : MonoBehaviour
{

    public float vertical, horizontal, moveAmount;
    public InputController inputController;
    Animator animator;
    public Vector3 rotationDirection;
    public Vector3 moveDirection;
    Transform cameraTransform;
    public float rotationSpeed = 0.4f;
    public CharacterStatus characterStatus;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        if (!inputController)
        {
            inputController = camera.GetComponent<InputController>();
        }
        if (!cameraTransform) {
            cameraTransform = camera.GetComponent<Transform>();
        }
        if (!characterStatus) {
            characterStatus = camera.GetComponent<PropertiesHolder>().characterStatus;
        }
    }

    // Update is called once per frame
    public void MoveUpdate()
    {

        vertical = inputController.vAxis;
        horizontal = inputController.hAxis;

        moveAmount = Mathf.Clamp01(Mathf.Abs(vertical) + Mathf.Abs(horizontal));

        //animator.SetFloat("vertical", moveAmount, 0.15f, Time.deltaTime);



        Vector3 moveDir = cameraTransform.forward * vertical;
        moveDir += cameraTransform.right * horizontal;



        

        moveDir.Normalize();
        moveDirection = moveDir;



        rotationDirection = cameraTransform.forward;
        RotationNoramal();
    }



    public void RotationNoramal()
    {
        if (!characterStatus.isMovingAiming)
        {
            rotationDirection = moveDirection;
        }

        Vector3 targetDir = rotationDirection;
        targetDir.y = 0;

        Quaternion lookDir = Quaternion.LookRotation(targetDir);
        Quaternion targetRot = Quaternion.Slerp(transform.rotation, lookDir, rotationSpeed);
        transform.rotation = targetRot;

    }
}