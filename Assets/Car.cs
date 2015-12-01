using UnityEngine;
using System.Collections;

public class Car : MonoBehaviour
{

    public WheelCollider[] WColForward;
    public WheelCollider[] WColBack;
    public GameObject[] FWheels;

    public GameObject camera;

    public Vector3 cameraRotationError;
    public Vector3 positionError;

    public float maxSteer = 30;
    public float speed = 25;
    public float maxBrake = 50;

    public Vector3 prevPosition;
    public Vector3 prevErrorPosition;
    public Vector3 curPosition;
    public Vector3 curErrorPosition;

    //public Vector3 prevRotation;
    //public Vector3 prevErrorRotation;
    public Vector3 curRotation;
    public Vector3 curErrorRotation;

    

    // Use this for initialization
    void Start()
    {
        gameObject.GetComponent<Rigidbody>().centerOfMass = new Vector3(0, -0.6f * 2.3f, 0);
        prevPosition = Vector3.zero;
        prevErrorPosition = Vector3.zero;

    }


    void Update()
    {

        if (menuScript.mode == menuScript.CarMode.Record)
        {
            float accel = Input.GetAxis("Vertical");
            float steer = Input.GetAxis("Horizontal");

            CarMove(accel, steer);
            rotateCamera();

            prevPosition = curPosition;
            prevErrorPosition = curErrorPosition;
            curPosition = transform.position;
            curErrorPosition = prevErrorPosition + (curPosition - prevPosition) + positionError;

            curErrorRotation = transform.rotation.eulerAngles + camera.transform.rotation.eulerAngles;
            curErrorRotation.x %= 360;
            curErrorRotation.y %= 360;
            curErrorRotation.z %= 360;
            curRotation = transform.rotation.eulerAngles;
        }

    }

    private void CarMove(float accel, float steer)
    {

        foreach (WheelCollider col in WColForward)
        {
            col.steerAngle = steer * maxSteer;
        }

        if (accel == 0)
        {
            foreach (WheelCollider col in WColBack)
            { 
                col.brakeTorque = maxBrake;
                col.motorTorque = 0;
            }
        }
        else
        {

            foreach (WheelCollider col in WColBack)
            {
                col.brakeTorque = 0;
                col.motorTorque = speed * (accel > 0 ? 1 : -1);
            }
        }
    }

    void rotateCamera()
    {
        float randError = (float)GetRandomNumber(-1, 1);

        Vector3 randomRotation = cameraRotationError * randError;

        randomRotation += camera.transform.eulerAngles;

        camera.transform.rotation = Quaternion.Euler(randomRotation);
    }

    public double GetRandomNumber(double minimum, double maximum)
    {
        System.Random random = new System.Random();
        return (random.NextDouble() * (maximum - minimum) + minimum) * (random.Next(2) == 1 ? 1 : -1);
    }
}
