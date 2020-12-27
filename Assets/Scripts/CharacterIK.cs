using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterIK : MonoBehaviour
{
    public Animator animator;
    public CharacterMovement characterMovement;
    public CharacterInventory characterInventory;
    public CharacterStatus characterStatus;
    public Transform targetLook;

    public Transform l_Hand;
    public Transform l_HandTarget;
    public Transform r_Hand;

    public Quaternion lh_rotation;

    public float rh_weight;

    public Transform shoulder;
    public Transform aimPivot;
    void Start()
    {
        animator = GetComponent<Animator>();
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        if (!characterStatus)
        {
            characterStatus = camera.GetComponent<PropertiesHolder>().characterStatus;
        }

        if (!characterMovement)
        {
            characterMovement = GetComponent<CharacterMovement>();
        }

        if (!characterInventory)
        {
            characterInventory = GetComponent<CharacterInventory>();
        }

        if (!targetLook)
        {
            targetLook = camera.GetComponent<CameraManger>().targetLook;
        }

        //---------------


        //l_HandTarget=characterInventory.actveWeapon.



        //---------------

        shoulder = animator.GetBoneTransform(HumanBodyBones.RightShoulder).transform;

        aimPivot = new GameObject().transform;
        aimPivot.name = "aim pivot";
        aimPivot.transform.parent = transform;

        r_Hand = new GameObject().transform;
        r_Hand.name = "right hand";
        r_Hand.transform.parent = aimPivot;


        l_Hand = new GameObject().transform;
        l_Hand.name = "left hand";
        l_Hand.transform.parent = aimPivot;

        r_Hand.localPosition = characterInventory.actveWeapon.rHandPos;
        Quaternion rotRight = Quaternion.Euler(characterInventory.actveWeapon.rHandRot.x, characterInventory.actveWeapon.rHandRot.y, characterInventory.actveWeapon.rHandRot.z);

        r_Hand.localRotation = rotRight;
    }

    // Update is called once per frame
    void Update()
    {
        lh_rotation = l_HandTarget.rotation;
        l_Hand.position = l_HandTarget.position;

        if (characterStatus.isAiming)
        {
            rh_weight += Time.deltaTime * 2;
        }
        else {
            rh_weight -= Time.deltaTime * 2;
        }

        rh_weight = Mathf.Clamp(rh_weight, 0, 1);
       
    }

    void OnAnimatorIK( )
    {
        aimPivot.position = shoulder.position;

        if (characterStatus.isAiming)
        {
            aimPivot.LookAt(targetLook);


            animator.SetLookAtWeight(1, 0.4f, 1);
            animator.SetLookAtPosition(targetLook.position);



        
        }
        else {
            animator.SetLookAtWeight(.3f, .1f, .3f);
            animator.SetLookAtPosition(targetLook.position);



           

        }
        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
        animator.SetIKPosition(AvatarIKGoal.LeftHand, l_Hand.position);
        animator.SetIKRotation(AvatarIKGoal.LeftHand, lh_rotation);


        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, rh_weight);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, rh_weight);
        animator.SetIKPosition(AvatarIKGoal.RightHand, r_Hand.position);
        animator.SetIKRotation(AvatarIKGoal.RightHand, r_Hand.rotation);
    }
}
