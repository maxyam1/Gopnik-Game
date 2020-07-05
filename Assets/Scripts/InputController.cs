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


    void Start()
    {
         fButton=false;
}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("x")) { xButton=true; }
        else{ xButton = false; }
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
    }
}
