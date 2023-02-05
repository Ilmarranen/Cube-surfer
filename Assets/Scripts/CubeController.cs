using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class CubeController : MonoBehaviour
{
    private StackController stackController;
    private MoveBack moveBack;
    private bool isStack=false;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Awake()
    {
        stackController = GameObject.FindObjectOfType<StackController>();
        moveBack = GetComponent<MoveBack>();
        rb = GetComponent<Rigidbody>();
        //To reduce bounciness of the cubes
        rb.maxDepenetrationVelocity = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {

    }

    //Using trigger collider to specify required collision zone
    //Only upper part of the cube is used so lowest cube won't fall out of stack on collision with walls
    //Raycast can be used as well though it can get expensive on many objects
    private void OnTriggerEnter(Collider other)
    {
        //If cube not in stack collides with player we add it to the player cube stack
        //Only parent element is tagged as player
        if (!isStack && other.transform.root.gameObject.CompareTag("Player"))
        {
            isStack = true;
            stackController.AddCube(gameObject);
        }

        //Collision with wall
        if (other.gameObject.CompareTag("CubeWall"))
        {
            isStack = false;
            //Shake camera during obstacle colission
            CameraShaker.Instance.ShakeOnce(2f, 2f, .1f, .5f);
            Handheld.Vibrate();
            stackController.RemoveCube(gameObject);
        }

        //If cube is part of the stack it will stay with player
        moveBack.enabled = !isStack;
    }
}
