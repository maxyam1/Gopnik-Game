using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Use : MonoBehaviour
{
    public float activateDistance;
    public Transform playerTransform;
    public GameObject mainCamera;
    public AllObjectsInfo allObjects;
    public Image readyToSitImg;
    public bool isReady;


    public Vector3 heading;

    public Vector3 car;
    public Vector3 player;

    public float X;

    private void Start()
    {
        //allObjects = camera.GetComponent<AllObjectsInfo>();
    }
    public void Update()
    {
        X = transform.position.x;
        car = transform.position;
        player = playerTransform.position;

        heading = transform.position -  playerTransform.position;

        if (heading.magnitude < activateDistance)
        {
            isReady = true;
        }
        else { isReady = false; }
    }
}
