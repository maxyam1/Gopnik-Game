using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
public PlayerController playerController;
    public void FixedUpdate()
    {
        playerController.MoveUpdate();
    }
}
