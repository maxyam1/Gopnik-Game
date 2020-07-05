using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarWaypoint : MonoBehaviour
{
    public CarWaypoint previousWaypoint;
    public CarWaypoint nextWaypoint;

    public bool isReadyToGo = true;

    public float maxSpeed = 10f;





    public List<CarWaypoint> branches;


    [Range(0f, 5f)]
    public float width = 1f;


    [Range(0f, 1f)]
    public float branchRatio = 0.5f;




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
            Vector3 destinationDirection = nextWaypoint.transform.position - transform.position;
            spawnedObject = Instantiate(prefab, transform.position, Quaternion.LookRotation(destinationDirection));
            isSpawned = true;
            spawnedTransform = spawnedObject.GetComponent<Transform>();
            CarMovement carMovement = spawnedObject.GetComponent<CarMovement>();
            carMovement.CurrentWaypoint(nextWaypoint);
            
        }
    }

    public void Delete()
    {
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
