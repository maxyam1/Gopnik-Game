using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class CrossRoads : MonoBehaviour
{
    public float greenTime = 20;
    public float cooldown = 3;

    public int flag = 0;

    public float timer = 0;

    public CarWaypoint h1;
    public CarWaypoint h2;
    public CarWaypoint v1;
    public CarWaypoint v2;


    // Start is called before the first frame update

    void h()
    { 
        if(h1)
            h1.isReadyToGo = true;
        if (h2)
            h2.isReadyToGo = true;
        if (v1)
            v1.isReadyToGo = false;
        if (v2)
            v2.isReadyToGo = false;


    }



    void v()
    {
        if (h1)
            h1.isReadyToGo = false;
        if (h2)
            h2.isReadyToGo = false;
        if (v1)
            v1.isReadyToGo = true;
        if (v2)
            v2.isReadyToGo = true;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (flag == 0 && timer >= cooldown)
        {
            timer = 0;
            flag = 1;
            h();
        }
        else if (flag == 1 && timer >= greenTime)
        {
            timer = 0;
            flag = 2;

            

        }
        else if (flag == 2 && timer >= cooldown) 
        {
            timer = 0;
            flag = 3;

            v();

        }
        else if (flag == 3 && timer >= greenTime)
        {
            timer = 0;
            flag = 0;

            

        }



    }
}
