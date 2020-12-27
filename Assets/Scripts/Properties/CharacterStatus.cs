using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character/status")]
public class CharacterStatus : ScriptableObject
{
    public bool isAiming;
    public bool isMovingAiming;
    public bool isSprinting;
    public bool isGround;
    public bool isInCar;
    public GameObject Car;
    public bool isAlive;
}

