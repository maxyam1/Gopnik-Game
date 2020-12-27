using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    // Start is called before the first frame update

    public float hAxis;
    public float vAxis;
    public bool xButton;
    public bool zButton;
    public bool shiftButton;

    public float xMouse;
    public float yMouse;

    public bool fButton;

    public bool leftMouseButton;
    public bool rightMouseButton;

    CharacterStatus characterStatus;
    Settings settings;
    PropertiesHolder propertiesHolder;
    public float distance;

    public CameraManger cameraManger;

    public Transform playerTransform;

    void Start()
    {
        if (!propertiesHolder) {
            propertiesHolder = GetComponent<PropertiesHolder>();
        }

        if (propertiesHolder)
        {
            characterStatus = propertiesHolder.characterStatus;
        }

        if (propertiesHolder) {
            settings = propertiesHolder.settings;
        }

        if (!cameraManger) {
            cameraManger = GetComponent<CameraManger>();
        }

        if (!playerTransform) {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        fButton = false;
    }

    // Update is called once per frame
    public void UpdateInput()
    {
        if (Input.GetKeyDown("x")) { xButton = true; }
        else { xButton = false; }
        if (Input.GetKeyDown("z")) { zButton = true; }
        else { zButton = false; }
        if (Input.GetKeyDown("f")) { fButton = true; }
        else { fButton = false; }
        if (Input.GetKeyDown("left shift")) { shiftButton = true; }
        if (Input.GetKeyUp("left shift")) { shiftButton = false; }
        hAxis = Input.GetAxis("Horizontal");
        vAxis = Input.GetAxis("Vertical");

        xMouse = Input.GetAxis("Mouse X");
        yMouse = Input.GetAxis("Mouse Y");


        leftMouseButton = Input.GetMouseButton(0);
        rightMouseButton = Input.GetMouseButton(1);

        characterStatus.isMovingAiming = rightMouseButton;

        characterStatus.isAiming = AbleToAiming() && rightMouseButton;
            
        
    }

    bool AbleToAiming()
    {
        distance = Vector3.Distance(playerTransform.position + playerTransform.up * 1.4f, cameraManger.targetLook.position);

        if (distance < settings.distanceStopAiming)
        {
            return false;
        }
        else {
            return true;
        }
    }
}
