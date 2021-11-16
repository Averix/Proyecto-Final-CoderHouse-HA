using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Current map waypoints
    [SerializeField] private GameObject [] waypoints;
    // Rotation Seed
    [SerializeField] private float lookSpeed = 0.00f;
    // Walking speed
    [SerializeField] private float speed = 0.0f;
    // Current position
    [SerializeField] public int nextWaypoint = 0;
    [SerializeField] public int newWP;
    [SerializeField] private int lastWaypoint = 0;


    // Variables to control by the GameManager
    // To control player motion and run animation
    [SerializeField] public bool isRun = false;
    // Amount of movement for the character this turn
    [SerializeField] public int movementPoints;
    // Target position to loop on this scenario
    [SerializeField] public int loopPosition;
    // Player Animator
    [SerializeField] public Animator playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isRun)
        {
            NextWaypoint();
            WayPointControl();
            LookAtObjective(nextWaypoint);
            ChaseObjective(nextWaypoint);
        }if (movementPoints == 0)
        {
            isRun = false;
            playerAnimator.SetTrigger("Idle");
        }
    }
    //look method using lerp
    private void LookAtObjective(int currentWP)
    {
        Quaternion newRotation = Quaternion.LookRotation(waypoints[currentWP].transform.position - transform.position);
        transform.localRotation = Quaternion.Lerp(transform.rotation, newRotation, lookSpeed * Time.deltaTime);
    }

    //movement method to chase the objective
    private void ChaseObjective(int currentWP)
    {
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWP].transform.position, speed * Time.deltaTime);
    }

    private void WayPointControl()
    {   
        if (newWP >= waypoints.Length)
        {
                newWP -= waypoints.Length;
                newWP += loopPosition;
        }
        if (nextWaypoint >= waypoints.Length)
        {
            nextWaypoint -= waypoints.Length;
            nextWaypoint += loopPosition;
        }
        if (lastWaypoint >= waypoints.Length)
        {
            lastWaypoint -= waypoints.Length;
            lastWaypoint += loopPosition;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.CompareTag("Platform"))
        {
            lastWaypoint ++;
            movementPoints--;
            if (lastWaypoint > waypoints.Length)
            {
                lastWaypoint -= waypoints.Length;
                lastWaypoint += loopPosition;
            }
        }
    }

    private void NextWaypoint()
    {
        if (nextWaypoint == lastWaypoint && movementPoints != 0)
        {
            nextWaypoint++;
        }
    }

}
