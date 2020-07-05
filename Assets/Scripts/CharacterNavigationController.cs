using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class CharacterNavigationController : MonoBehaviour
{

    public float movementSpeed;
    public float rotationSpeed;
    public float stopDistance;

    public Animator animator;

    public Vector3 destination;

    Vector3 velocity;

    public bool reachedDestination;

    public Vector3 lastPosition;

    bool isDied;

    public bool isInCar;

    CapsuleCollider capsuleCollider;

    //Rigidbody rigidbody;

    void Start()
    {
        isDied = false;
        isInCar = false;
        capsuleCollider = GetComponent<CapsuleCollider>();

        movementSpeed = Random.Range(1f, 2f);

       // rigidbody = GetComponent<Rigidbody>();
         
        //animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position != destination) {

            if (isDied == false)
            {
                Vector3 destinationDirection = destination - transform.position;
                destinationDirection.y = 0;

                float destinationDistance = destinationDirection.magnitude;

                if (destinationDistance >= stopDistance)
                {
                    reachedDestination = false;
                    Quaternion targetRotation = Quaternion.LookRotation(destinationDirection);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                    transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
                }
                else
                {
                    reachedDestination = true;
                }


                velocity = (transform.position - lastPosition) / Time.deltaTime;

                //velocity = rigidbody.velocity;

                velocity.y = 0;
                var velocityMagnitude = velocity.magnitude;
                velocity = velocity.normalized;
                var fwdDotProduct = Vector3.Dot(transform.forward, velocity);
                var rightDotProduct = Vector3.Dot(transform.right, velocity);



                animator.SetFloat("horizonatal", rightDotProduct);
                animator.SetFloat("vertical", fwdDotProduct);
            }

        }

        
    }

    public void Die() {
        isDied = true;

        capsuleCollider.height = 1.5f;
        capsuleCollider.radius = 0.2f;
        capsuleCollider.direction = 2;
        capsuleCollider.center = new Vector3(capsuleCollider.center.x, 0.35f, capsuleCollider.center.z);

        animator.SetBool("die", true);
    }

    public void SetDestination(Vector3 destination) {
        this.destination = destination;
        reachedDestination = false;
    }
}
