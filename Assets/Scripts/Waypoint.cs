using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public Waypoint previousWaypoint;
    public Waypoint nextWaypoint;


    public List<Waypoint> branches;


    [Range(0f, 5f)]
    public float width = 1f;


    [Range(0f, 1f)]
    public float branchRatio = 0.5f;


    public Zebra zebra;


    public bool isSpawner = false;

    public bool isSpawned = false;

    public GameObject spawnedObject;

    public Transform spawnedTransform;

    public GameObject prefab;

    public Vector3 GetPosition()
    {
        Vector3 minBound = transform.position + transform.right * width / 2f;
        Vector3 maxBound = transform.position - transform.right * width / 2f;

        return Vector3.Lerp(minBound, maxBound, Random.Range(0f, 1f));
    }

    public void Spawn()
    {
        if (isSpawned == false)
        {
            spawnedObject = Instantiate(prefab, transform.position, transform.rotation);
            isSpawned = true;
            spawnedTransform = spawnedObject.GetComponent<Transform>();
            WaypointNavigator waypointNavigator = spawnedObject.GetComponent<WaypointNavigator>();
            if (nextWaypoint != null)
            {
                waypointNavigator.CurrentWaypoint(nextWaypoint);
            }
            else { 
            waypointNavigator.CurrentWaypoint(previousWaypoint);
            }
            
        }
    }

    public void Delete() {
        if (isSpawned == true)
        {
            Destroy(spawnedObject);
            isSpawned = false;
            spawnedTransform = null;
        }
    }

    public Vector3 GetStaticPosition()
    {
        return transform.position;
    }
}