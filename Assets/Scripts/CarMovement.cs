using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.WSA.Input;

public class CarMovement : MonoBehaviour
{
    // Start is called before the first frame update

    //public float temp;

    //public float offset;

    //public int gear = 0;

    //public int maxRPM = 5000;

    //public float[] powerGear = new float[] {0, 5, 3.5f, 2.5f, 2};


    // public float[] delta = new float[] { };


    //public int maxGear = 4;
    // public int minGear = -1;
    public Vector3 centerOfMass = new Vector3(0,0.5f,0);

    public GameObject FL;
    public GameObject FR;
    public GameObject RL;
    public GameObject RR;

    public GameObject FLtexture;
    public GameObject FRtexture;
    public GameObject RLtexture;
    public GameObject RRtexture;

    Transform FLtr;
    Transform FRtr;
    Transform RLtr;
    Transform RRtr;

    WheelCollider FL_collider;
    WheelCollider FR_collider;
    WheelCollider RL_collider;
    WheelCollider RR_collider;

    public bool isDriveble;
    public bool isActive;

    public GameObject mainCamera;
    InputController inputController;

    Rigidbody carRigidbody;

    public float wheelAngle = 40;
    public float wheelBrake = 400;
    public float wheelPower = 400;

    //public float engineRpm;

    public float rpm;
    public float torque;


    //public Transform TEMP;

    public float angle;


    public Vector3 destination;
    public bool reachedDestination;
    public float stopDistance;

    Quaternion targetRotation;

    public float maxSpeed;

    public CarWaypoint currentWaypoint;

    public float rayCastDistance = 5;

    bool rayCastActive;


    void UpadateWheelPose(WheelCollider col, Transform t) {
        Vector3 pos = t.position;
        Quaternion rot = t.rotation;
        col.GetWorldPose(out pos, out rot);
        t.position = pos;
    }


    void Start()
    {
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().centerOfMass = centerOfMass;
        }
        FL_collider = FL.GetComponent<WheelCollider>();
        FR_collider = FR.GetComponent<WheelCollider>();
        RL_collider = RL.GetComponent<WheelCollider>();
        RR_collider = RR.GetComponent<WheelCollider>();
        carRigidbody = GetComponent<Rigidbody>();
        FLtr = FLtexture.GetComponent<Transform>();
        FRtr = FRtexture.GetComponent<Transform>();
        RLtr = RLtexture.GetComponent<Transform>();
        RRtr = RRtexture.GetComponent<Transform>();
        if(mainCamera !=null)
            inputController = mainCamera.GetComponent<InputController>();
        isActive = false;

        if(currentWaypoint!=null)
            SetDestination(currentWaypoint.GetPosition());
    }



    /* void ActivTrue() {
         isActive = true;
     }

     void ActivFalse()
     {
         isActive = false;

     }*/

    public void CurrentWaypoint(CarWaypoint waypoint)
    {
        currentWaypoint = waypoint;
        SetDestination(currentWaypoint.GetPosition());
    }

    void Update()
    {
        
        if (isDriveble == true)
        {
            if (isActive == true)
            {
                angle = inputController.hAxis * wheelAngle;
                FL_collider.steerAngle = angle;
                FR_collider.steerAngle = angle;

                FLtr.localEulerAngles = new Vector3(0, angle, 0);
                FRtr.localEulerAngles = new Vector3(0, 180 + angle, 0);


                float vAxis = inputController.vAxis;

                //rpm = RR_collider.rpm;




                UpadateWheelPose(FL_collider, FLtr);
                UpadateWheelPose(FR_collider, FRtr);
                UpadateWheelPose(RL_collider, RLtr);
                UpadateWheelPose(RR_collider, RRtr);








                if (inputController.shiftButton == true)
                {
                    FL_collider.brakeTorque = wheelBrake;
                    FR_collider.brakeTorque = wheelBrake;
                    RL_collider.brakeTorque = wheelBrake;
                    RR_collider.brakeTorque = wheelBrake;
                }
                else
                {
                    FL_collider.brakeTorque = 0;
                    FR_collider.brakeTorque = 0;
                    RL_collider.brakeTorque = 0;
                    RR_collider.brakeTorque = 0;
                }


                torque = vAxis * wheelPower;

                RL_collider.motorTorque = torque;
                RR_collider.motorTorque = torque;

            }
            else
            {
                RL_collider.motorTorque = 0;
                RR_collider.motorTorque = 0;
            }
        }
        else {
            
            Vector3 destinationDirection = destination - transform.position;
            destinationDirection.y = 0;

            float destinationDistance = destinationDirection.magnitude;





            float angle = Vector3.SignedAngle(destinationDirection, transform.forward, transform.up) * -1;

            if (Mathf.Abs(angle) > wheelAngle)
            {
                if (angle > 0)
                {
                    angle = wheelAngle;
                }
                else
                {
                    angle = -wheelAngle;
                }
            }






            // Bit shift the index of the layer (8) to get a bit mask
            //int layerMask = 1 << 8;

            // This would cast rays only against colliders in layer 8.
            // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
            //layerMask = ~layerMask;

            RaycastHit hit;

            //Vector3 from = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z+5f);
            //Vector3 to = transform.TransformDirection(Vector3.forward);
            //to.y = to.y + 1f;
            //to.z = to.z + 5f;

            Vector3 up = new Vector3(0, 1, 0);

            Ray ray = new Ray(transform.position + up, transform.forward);
            ray.direction = destinationDirection;

            float RCD = rayCastDistance + carRigidbody.velocity.magnitude * 0.5f;

            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(ray, out hit, RCD))
            {
                Debug.DrawRay(transform.position + up, ray.direction.normalized * hit.distance, Color.yellow);
                //Debug.Log("Did Hit");
                rayCastActive = true;
            }
            else
            {
                rayCastActive = false;
                Debug.DrawRay(transform.position+up, ray.direction.normalized * RCD, Color.white);
                //Debug.Log("Did not Hit");
            }




            if (reachedDestination == false && currentWaypoint.isReadyToGo == true && rayCastActive==false)
            {

                if (destinationDistance >= stopDistance) //&& reachedDestination == false)
                {
                    reachedDestination = false;

                    /*if (Vector3.Angle(destinationDirection, transform.forward) <= wheelAngle)
                    {

                        targetRotation = Quaternion.LookRotation(destinationDirection);



                    }

                    FLtr.transform.rotation = Quaternion.RotateTowards(FLtr.transform.rotation, targetRotation, 200 * Time.deltaTime);
                    FRtr.transform.rotation = FLtr.transform.rotation;
                    FRtr.eulerAngles = new Vector3(0, 180 + FRtr.eulerAngles.y, 0);

                    FL_collider.steerAngle = FLtr.transform.localEulerAngles.y;
                    FR_collider.steerAngle = FRtr.transform.localEulerAngles.y;
*/
                    
                    
                    FL_collider.steerAngle = angle;
                    FR_collider.steerAngle = angle;

                    FLtr.localEulerAngles = new Vector3(0, angle, 0);
                    FRtr.localEulerAngles = new Vector3(0, 180 + angle, 0);


                    if (carRigidbody.velocity.magnitude > maxSpeed || reachedDestination == true)
                    {
                        FL_collider.brakeTorque = wheelBrake;
                        FR_collider.brakeTorque = wheelBrake;
                        RL_collider.brakeTorque = wheelBrake;
                        RR_collider.brakeTorque = wheelBrake;


                        RL_collider.motorTorque = 0;
                        RR_collider.motorTorque = 0;

                    }
                    else
                    {



                        FL_collider.brakeTorque = 0;
                        FR_collider.brakeTorque = 0;
                        RL_collider.brakeTorque = 0;
                        RR_collider.brakeTorque = 0;


                        RL_collider.motorTorque = wheelPower;
                        RR_collider.motorTorque = wheelPower;

                    }

















                    /*
                    //float angle = Vector3.Angle(destinationDirection,transform.forward);
                    angle = targetRotation.eulerAngles.y;

                    print(angle);

                    if (Mathf.Abs(angle) > wheelAngle)
                    {
                        if (angle >= 180) {
                            angle = wheelAngle;
                        }
                        else {
                            angle = -wheelAngle;
                        }


                    }
                    FL_collider.steerAngle = angle;
                    FR_collider.steerAngle = angle;



                    //FLtr.localEulerAngles = new Vector3(0, angle, 0);
                    //FRtr.localEulerAngles = new Vector3(0, 180 + angle, 0);

                */

                    // transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                    // transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
                }
                else
                {
                    reachedDestination = true;



                    bool shouldBranch = false;

                    if (currentWaypoint.branches != null && currentWaypoint.branches.Count > 0)
                    {
                        shouldBranch = Random.Range(0f, 1f) <= currentWaypoint.branchRatio ? true : false;
                    }

                    if (shouldBranch)
                    {
                        currentWaypoint = currentWaypoint.branches[Random.Range(0, currentWaypoint.branches.Count - 1)];


                        SetDestination(currentWaypoint.GetPosition());
                        maxSpeed = currentWaypoint.maxSpeed;
                    }
                    else
                    {

                        if (currentWaypoint.nextWaypoint != null)
                        {
                            currentWaypoint = currentWaypoint.nextWaypoint;
                            SetDestination(currentWaypoint.GetPosition());
                            maxSpeed = currentWaypoint.maxSpeed;
                        }

                    }
                }

            }
            else {
                FL_collider.brakeTorque = wheelBrake;
                FR_collider.brakeTorque = wheelBrake;
                RL_collider.brakeTorque = wheelBrake;
                RR_collider.brakeTorque = wheelBrake;


                RL_collider.motorTorque = 0;
                RR_collider.motorTorque = 0;

            }

        }
    }

    public void SetDestination(Vector3 destination)
    {
        this.destination = destination;
        reachedDestination = false;
    }





   












    /*if (vAxis == 0)
    {
        FL_collider.brakeTorque = 0;
        FR_collider.brakeTorque = 0;
        RL_collider.brakeTorque = 0;
        RR_collider.brakeTorque = 0;
    }
    else
    {

        if (rpm < smallestSpeed && rpm > -smallestSpeed)
        {
            RL_collider.motorTorque = vAxis * wheelPower;
            RR_collider.motorTorque = vAxis * wheelPower;
        }
        else if (rpm > smallestSpeed)
        {
            if (vAxis >= 0)
            {
                FL_collider.brakeTorque = 0;
                FR_collider.brakeTorque = 0;
                RL_collider.brakeTorque = 0;
                RR_collider.brakeTorque = 0;
                float torque = vAxis;
                RL_collider.motorTorque = torque * wheelPower;
                RR_collider.motorTorque = torque * wheelPower;
            }
            else
            {
                float brake = wheelBrake * -vAxis;
                FL_collider.brakeTorque = brake;
                FR_collider.brakeTorque = brake;
                RL_collider.brakeTorque = brake;
                RR_collider.brakeTorque = brake;


            }
        }
        else if (rpm < -smallestSpeed)
        {
            if (vAxis > 0)
            {
                float brake = wheelBrake * vAxis;
                FL_collider.brakeTorque = brake;
                FR_collider.brakeTorque = brake;
                RL_collider.brakeTorque = brake;
                RR_collider.brakeTorque = brake;
            }
            else
            {
                FL_collider.brakeTorque = 0;
                FR_collider.brakeTorque = 0;
                RL_collider.brakeTorque = 0;
                RR_collider.brakeTorque = 0;
                float torque = vAxis;
                RL_collider.motorTorque = torque * wheelPower;
                RR_collider.motorTorque = torque * wheelPower;
            }
        }
    }*/


}
