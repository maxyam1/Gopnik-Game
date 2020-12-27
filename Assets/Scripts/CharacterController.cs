using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    // Start is called before the first frame update
    CharacterMovement characterMovement;
    CharacterAnimation characterAnimation;
    InputController inputController;
    void Start()
    {
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");

        characterMovement = GetComponent<CharacterMovement>();
        characterAnimation = GetComponent<CharacterAnimation>();
        inputController = camera.GetComponent<InputController>();
    }

    // Update is called once per frame
    void Update()
    {
        characterMovement.MoveUpdate();
        characterAnimation.UpdateAnimation();
        inputController.UpdateInput();
    }
}
