using UnityEngine;
using System.Collections;
using System;

public class ObstacleAvoidance : MonoBehaviour 
{
    [SerializeField]
    private float movementSpeed = 20.0f;
    [SerializeField]
    private float rotationSpeed = 5.0f;
    [SerializeField]
    private float force = 50.0f;
    [SerializeField]
    private float minimumAvoidanceDistance = 20.0f;
    [SerializeField]
    private float toleranceRadius = 3.0f;

    private float currentSpeed;
    private Vector3 targetPoint;
    private Vector3 direction;
    private Quaternion targetRotation;

    // Use this for initialization
    void Start () 
    {
        targetPoint = Vector3.zero;
    }

 
	// Update is called once per frame
	private void Update () 
    {
        CheckInput();
        MoveAgent(); 
	}

    /// <summary>
    /// This is the function that will handle the movement of the vehicle
    /// No need to change anything in this function, unless you need to
    /// </summary>
    private void MoveAgent()
    {
        //Calculate the directional vector towards the target point
        direction = targetPoint - transform.position;
        //Normalize the directional vector so that it's magniture is no more than 1.0
        direction.Normalize();

        //Modify the direction by applying obstacle avoidance
        ApplyAvoidance(ref direction);

        if (Vector3.Distance(targetPoint, transform.position) < toleranceRadius)
            return;

        currentSpeed = movementSpeed * Time.deltaTime;

        targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        transform.position += transform.forward * currentSpeed;
    }

    /// <summary>
    /// This is the function that will handle the user's input
    /// No need to change anything in this function, unless you need to
    /// </summary>
    private void CheckInput()
    {
        //Check whether the user has clicked the left mouse button
        if (Input.GetMouseButtonDown(0))
        {
            //Cast a ray from the main camera to the position of the mouse
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit mouseHit;
            //If the ray hits something, assign the hit point as the target point
            if (Physics.Raycast(ray, out mouseHit, 100.0f))
            {
                targetPoint = mouseHit.point;
            }
        }
    }

    private void ApplyAvoidance(ref Vector3 direction)
    {
        //TODO
        //1. Cast a raycast that will only hit the Obstacle Layer
        //2. The raycast's origin will start at the position of the player towards its forward vector
        //3. When the raycast hits an obstacle, get the normal of the surface hit and calculate the new direction vector

    }
}