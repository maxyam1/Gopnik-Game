using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    Vector3 lastPos;
    public GameObject decal;

    public GameObject metalHitEffect;
    public GameObject sandHitEffect;
    public GameObject stoneHitEffect;
    public GameObject[] meatHitEffect;
    public GameObject woodHitEffect;

    void Start()
    {
        lastPos = transform.position;
        Destroy(gameObject, 10);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        RaycastHit hit;

        Debug.DrawLine(lastPos, transform.position);

        if (Physics.Linecast(lastPos, transform.position, out hit)) {

           
            if (hit.collider.sharedMaterial != null)
            {
                string materialName = hit.collider.sharedMaterial.name;
                switch (materialName) {
                    case "Metal":
                        SpawnDecals(hit, metalHitEffect);
                        break;
                    case "Sand":
                        SpawnDecals(hit, sandHitEffect);
                        break;
                    case "Stone":
                        SpawnDecals(hit, stoneHitEffect);
                        break;
                    case "Wood":
                        SpawnDecals(hit, woodHitEffect);
                        break;
                    case "Meat":
                        SpawnDecals(hit, meatHitEffect[Random.Range(0, meatHitEffect.Length)]);
                        break;
                }
            }
            Destroy(gameObject);
        }

        lastPos = transform.position;
    }





    void SpawnDecals(RaycastHit hit,GameObject prefab) {
        GameObject spawnDecal = GameObject.Instantiate(prefab, hit.point, Quaternion.LookRotation(hit.normal));
        spawnDecal.transform.SetParent(hit.collider.transform);
        Destroy(spawnDecal.gameObject, 10);
    }


}
