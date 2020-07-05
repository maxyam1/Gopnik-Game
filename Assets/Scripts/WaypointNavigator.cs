using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class WaypointNavigator : MonoBehaviour
{
    CharacterNavigationController controller;
    public Waypoint currentWaypoint;

    public int Direction = 0;

    private void Awake()
    {
       

        controller = GetComponent<CharacterNavigationController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if(currentWaypoint!=null)
            controller.SetDestination(currentWaypoint.GetPosition());


        Direction = Random.Range(-100, 99);
        if (Direction >= 0)
        {
            Direction = 0;
        }
        else {
            Direction = 1;
        }
    }

    // Update is called once per frame



    public void CurrentWaypoint(Waypoint waypoint) {
        currentWaypoint = waypoint;
        controller.SetDestination(currentWaypoint.GetPosition());
    }

    void Update()
    {


            //controller.SetDestination(currentWaypoint.GetPosition());
            if (controller.reachedDestination)
            {

                bool shouldBranch = false;

                if (currentWaypoint.branches != null && currentWaypoint.branches.Count > 0)
                {
                    shouldBranch = Random.Range(0f, 1f) <= currentWaypoint.branchRatio ? true : false;
                }

                if (shouldBranch)
                {
                    currentWaypoint = currentWaypoint.branches[Random.Range(0, currentWaypoint.branches.Count - 1)];

                    if (currentWaypoint.zebra != null)
                    {
                        currentWaypoint.zebra.Activate();


                    }
                }
                else
                {


                    if (Direction == 0)
                    {
                        if (currentWaypoint.nextWaypoint != null)
                        {
                            currentWaypoint = currentWaypoint.nextWaypoint;
                        }
                        else
                        {
                            currentWaypoint = currentWaypoint.previousWaypoint;
                            Direction = 1;
                        }
                    }

                    if (Direction == 1)
                    {
                        if (currentWaypoint.previousWaypoint != null)
                        {
                            currentWaypoint = currentWaypoint.previousWaypoint;
                        }
                        else
                        {
                            currentWaypoint = currentWaypoint.nextWaypoint;
                            Direction = 0;
                        }
                    }
                }
                controller.SetDestination(currentWaypoint.GetPosition());
            }
        
    }
}
