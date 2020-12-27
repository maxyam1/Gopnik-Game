using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public WeaponProperties weaponProperties;
    public CharacterStatus characterStatus;
    public InputController inputController;
    public Transform shotPoint;
    public Transform targetLook;

    public GameObject cameraMain;
    //public GameObject decal;

    public GameObject bullet;


    public ParticleSystem muzzleFlash;
    public GameObject cartridge;
    public Transform cartridgeSpawner;
    AudioSource audioSource;
    public AudioClip shootClip;
    
    void Start()
    {
        cameraMain = GameObject.FindGameObjectWithTag("MainCamera");
        
        if (!characterStatus)
        {
            characterStatus = cameraMain.GetComponent<PropertiesHolder>().characterStatus;
        }
        if (!inputController)
        {
            inputController = cameraMain.GetComponent<InputController>();
        }

        audioSource = GetComponent<AudioSource>();

        targetLook = cameraMain.GetComponent<CameraManger>().targetLook;
    }

   
    void Update()
    {
        shotPoint.LookAt(targetLook);

        Vector3 origin = shotPoint.position;
        Vector3 dir = targetLook.position;


        /*
        RaycastHit hit;

        Debug.DrawLine(origin, dir, Color.black);
        Debug.DrawLine(cameraMain.transform.position, dir, Color.black);

        decal.SetActive(false);

        if (Physics.Linecast(origin, dir, out hit)) {
            decal.SetActive(true);
            decal.transform.position = hit.point + hit.normal * 0.01f;
            decal.transform.rotation = Quaternion.LookRotation(hit.normal);

        }*/

        if (characterStatus.isAiming && inputController.leftMouseButton) {
            Shoot();
        }

    }


    public void Shoot() {
        Instantiate(bullet, shotPoint.position, shotPoint.rotation);
        audioSource.PlayOneShot(shootClip);
        muzzleFlash.Play();
        CartridgeInstantiate();


    }


    void CartridgeInstantiate() {
        GameObject newCartridge = Instantiate(cartridge);
        newCartridge.transform.position = cartridgeSpawner.position;

        Quaternion rot = cartridgeSpawner.rotation;
        newCartridge.transform.rotation = rot;

        newCartridge.transform.parent = null;
        newCartridge.GetComponent<Rigidbody>().AddForce(newCartridge.transform.forward * Random.Range(0.5f, 1f));

        Destroy(newCartridge, 10);

    }
}
