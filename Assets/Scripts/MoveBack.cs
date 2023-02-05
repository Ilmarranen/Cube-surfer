using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script for moving back objects to create illusion on movement ahead for player
//Using same method as with infinite runners for easier infinite track
//Unfortunately cube trail is harder with this method
public class MoveBack : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] float leftBound = -15;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //Move object back at set speed if playing
        if (gameManager.isActive)
        {
            transform.Translate(Vector3.back * Time.deltaTime * gameManager.speed);
        }

        //Destroy objects out of reach of camera (not the track itself, obviously)
        if (transform.position.z < leftBound && !transform.CompareTag("Track"))
        {
            Destroy(gameObject);
        }
    }
}
