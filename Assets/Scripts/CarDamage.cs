using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDamage : MonoBehaviour
{
    // Start is called before the first frame update

    public float kDamage;
    public CharacterStatus characterStatus;
    public Rigidbody rigidbody;
    public float smallestSpeed;
    void Start()
    {
        
    }

    // Update is called once per frame

    float modul(Vector3 v) {
        return Mathf.Sqrt(v.x * v.x + v.y * v.y + v.z * v.z);
    }
    private void OnTriggerStay(Collider other)
    {
        
        HP hp = other.GetComponent<HP>();
        if (hp) {
            if (other.tag == "Player")
            {
                if (characterStatus.isInCar == false)
                {
                    float damage = (modul(rigidbody.velocity) - smallestSpeed) * kDamage;
                    if (damage > 0)
                    {
                        hp.Change(damage);
                    }
                }
            }
            else {
                CharacterNavigationController characterNavigationController = other.GetComponent<CharacterNavigationController>();
                if (characterNavigationController.isInCar == false) {
                    float damage = (modul(rigidbody.velocity) - smallestSpeed) * kDamage;
                    if (damage > 0)
                    {
                        hp.Change(damage);
                    }
                }
            
            }
        }
    }
}
