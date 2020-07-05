using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zebra : MonoBehaviour
{

    public CarWaypoint carRoad1;
    public CarWaypoint carRoad2;

    public float cooldown;
    float timer = 0;

    public bool isActive = false;

    // Update is called once per frame
    void Update()
    {
        if (isActive == true) {
            timer += Time.deltaTime;
            if (timer >= cooldown) {
                
                CarGo();
            }
        
        }
    }


    public void Activate() {
        timer = 0;
        isActive = true;
        CarStop();
    }

    void CarStop() {
        carRoad1.isReadyToGo = false;
        carRoad2.isReadyToGo = false;

    }

    void CarGo()
    {
        timer = 0;
        carRoad1.isReadyToGo = true;
        carRoad2.isReadyToGo = true;
        isActive = false;
    }
}
