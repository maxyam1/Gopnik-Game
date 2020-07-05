using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficSpawner : MonoBehaviour
{

    public float minLoadDistance = 50;
    public float maxLoadDistance = 100;

    public int maxTrafficPerson = 20;
    public int maxTrafficCars = 12;

    int TrafficPerson;
    int TrafficCars;

    class waypointsStruct {
        public Waypoint waypoint;
        public Transform transform;
        public bool isActive;
    }

    class carWaypointsStruct
    {
        public CarWaypoint carWaypoint;
        public Transform transform;
        public bool isActive;
    }


    List<waypointsStruct> waypoints = new List<waypointsStruct>();
    List<carWaypointsStruct> carWaypoints=new List<carWaypointsStruct>();

    GameObject[] waypointsGO;
    GameObject[] carWaypointsGO;

    GameObject[] gameObjects;
    void Start()
    {
        waypointsGO = GameObject.FindGameObjectsWithTag("Waypoint");

        TrafficPerson = 0;
        TrafficCars = 0;

        //gameObjects = GameObject.FindGameObjectsWithTag("Untagged");
        /*
        int temp8 = 0;
        int temp9 = 0;

        foreach (GameObject gameObject in gameObjects) {
            if (gameObject.layer == 8)
            {
                waypointsGO[temp8] = gameObject;
                temp8++;
            }
            else if (gameObject.layer == 9) {
                carWaypointsGO[temp9] = gameObject;
                temp9++;
            }                 
        }
        */

        if (waypointsGO != null)
        {
            foreach (GameObject gameObject in waypointsGO)
            {
                Waypoint temp = gameObject.GetComponent<Waypoint>();
                if (temp != null && temp.isSpawner == true)
                {

                    waypointsStruct str = new waypointsStruct();
                    str.waypoint = temp;
                    str.transform = gameObject.GetComponent<Transform>();
                    str.isActive = false;
                    waypoints.Add(str);
                }
            }
        }
        carWaypointsGO = GameObject.FindGameObjectsWithTag("CarWaypoint");

        if (carWaypointsGO != null)
        {
            foreach (GameObject gameObject in carWaypointsGO)
            {
                CarWaypoint temp = gameObject.GetComponent<CarWaypoint>();
                if (temp != null && temp.isSpawner == true)
                {
                    carWaypointsStruct str = new carWaypointsStruct();
                    str.carWaypoint = temp;
                    str.transform = gameObject.GetComponent<Transform>();
                    str.isActive = false;
                    carWaypoints.Add(str);
                }
            }
        }
    }




    void Activate1(waypointsStruct wayP,bool isActive) {
        wayP.isActive = isActive;

    }

    void Activate2(carWaypointsStruct wayP, bool isActive)
    {
        wayP.isActive = isActive;

    }




    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        Vector3 Distance1;
        Vector3 Distance2;



      

        if (waypoints != null)
        {
            foreach (waypointsStruct wayP in waypoints)
            {
                Distance1 = pos - wayP.transform.position;
               


                if (wayP.waypoint.spawnedTransform != null)
                {
                    Distance2 = pos - wayP.waypoint.spawnedTransform.position;
                }
                else
                {
                    Distance2 = new Vector3(0, 0, 0);
                }


                if (Distance1.magnitude > minLoadDistance && wayP.isActive == false && Distance1.magnitude < maxLoadDistance && TrafficPerson < maxTrafficPerson)
                {
                    wayP.waypoint.Spawn();
                    Activate1(wayP, true);
                    TrafficPerson++;
                }
                else if (Distance2.magnitude > maxLoadDistance && wayP.isActive == true)
                {
                    wayP.waypoint.Delete();
                    Activate1(wayP, false);
                    TrafficPerson--;
                }

            }

        }


        if (carWaypoints != null)
        {
            foreach (carWaypointsStruct wayP in carWaypoints)
            {
                Distance1 = pos - wayP.transform.position;
                
                if (wayP.carWaypoint.spawnedTransform != null)
                {
                    Distance2 = pos - wayP.carWaypoint.spawnedTransform.position;
                }
                else {
                    Distance2 = new Vector3(0, 0, 0);
                }

                if (Distance1.magnitude > minLoadDistance && wayP.isActive == false && Distance1.magnitude < maxLoadDistance && TrafficCars < maxTrafficCars)
                {
                    wayP.carWaypoint.Spawn();
                    Activate2(wayP, true);
                    wayP.isActive = true;
                    TrafficCars++;
                }
                else if (Distance2.magnitude > maxLoadDistance && wayP.isActive == true)
                {
                    wayP.carWaypoint.Delete();
                    Activate2(wayP, false);
                    TrafficCars--;
                }

            }
        }

    }
}
