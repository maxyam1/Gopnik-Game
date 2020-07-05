using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllObjectsInfo : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Player;
    public Transform playerTransform;
    public Image readyToSitImg;
    private void Start()
    {
        playerTransform = Player.GetComponent<Transform>();
        
    }
}
