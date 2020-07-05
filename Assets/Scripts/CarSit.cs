
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarSit : MonoBehaviour
{
    public Transform exitTransform;
    //public Transform plTransform;
    CarMovement carMovement;
    //public GameObject cameraPivot;
    public CameraConfig cameraConfig;
    public CameraManger cameraManger;
    public InputController inputController;
    public CharacterStatus characterStatus;
    public PlayerController playerController;
    public float activateDistance;
    public GameObject playerObj;
    Transform playerTransform;
    public GameObject mainCamera;
    //public AllObjectsInfo allObjects;
    public Image readyToSitImg;
    public bool isReady;
    
    public bool carActive;

    public float dist;


    public Vector3 heading;

    public Vector3 car;
    public Vector3 player;

    public float X;

    private void Start()
    {
        carActive = false;
        playerTransform = playerObj.GetComponent<Transform>();
        carMovement = GetComponent<CarMovement>();
        
        //allObjects = camera.GetComponent<AllObjectsInfo>();
    }
    public void Update()
    {
        X = transform.position.x;
        car = transform.position;
        player = playerTransform.position;

        heading = transform.position - playerTransform.position;

        dist = heading.sqrMagnitude;

        if (dist < activateDistance* activateDistance)
        {
            isReady = true;
            readyToSitImg.fillAmount = 1;
            if (characterStatus.isInCar == false && inputController.fButton == true) {
                
                characterStatus.Car = this.gameObject;
                playerController.SittingCar();
                cameraManger.SetCameraAtVaz();
                carMovement.isActive = true;
                
            }
            else if (characterStatus.isInCar == true && inputController.fButton == true)
            {
                
                characterStatus.Car = null;
                playerController.GetOutCar(exitTransform.position);
                cameraManger.SetCameraAtPlayer();
                carMovement.isActive = false;

            }
        }
        else
        { 
            isReady = false;
            readyToSitImg.fillAmount = 0;
            
        }

    }
}