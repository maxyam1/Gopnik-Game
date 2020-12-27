using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    public Animator animator;
    public CharacterMovement characterMovement;
    public CharacterStatus characterStatus;
    public InputController inputController;
    new GameObject camera;



    void Start()
    {
         camera = GameObject.FindGameObjectWithTag("MainCamera");

        if (!animator) {
            animator = GetComponent<Animator>();
        }
        if (!characterMovement) {
            characterMovement = GetComponent<CharacterMovement>();
        }
        if (!characterStatus) {
            characterStatus = camera.GetComponent<PropertiesHolder>().characterStatus;
        }
        if (!inputController) {
            inputController = camera.GetComponent<InputController>();
        }

    }

    public void UpdateAnimation()
    {
        animator.SetBool("isSprinting", characterStatus.isSprinting);
        animator.SetBool("isAiming", characterStatus.isAiming);
        animator.SetBool("isMovingAiming", characterStatus.isMovingAiming);

        if (!characterStatus.isMovingAiming)
            AnimationNormal();
        else
            AnimationAiming();



    }



    void AnimationNormal() {
        animator.SetFloat("vertical", characterMovement.moveAmount, 0.15f, Time.deltaTime);
    }


    void AnimationAiming() {
        float v = inputController.vAxis;
        float h = inputController.hAxis;

        animator.SetFloat("vertical", v, 0.15f, Time.deltaTime);
        animator.SetFloat("horizontal", h, 0.15f, Time.deltaTime);
    }





}
