using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackController : MonoBehaviour
{

    private Vector3 startPos;
    private float repeatWidth;

    //Track generation
    [SerializeField] float startPosition = 25, trackSegmentLength = 20;
    [SerializeField] int pickupsQuantity = 3, wallMaxSize = 6, wallBlindZone = 3;
    [SerializeField] GameObject pickupPrefab, wallPrefab;
    private float trackLength, trackWidth, cubeSizeOffset = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        //Position of track at the start of the game
        startPos = transform.position;
        //Getting track size by checking collider size and upscaling it
        trackLength = GetComponent<BoxCollider>().size.z * transform.localScale.z;
        trackWidth = GetComponent<BoxCollider>().size.x * transform.localScale.x;
        //Getting size for repeating as half length
        repeatWidth = trackLength / 2;
        //Spawn starting objects on track
        GenerateTrack(startPosition);
    }

    // Update is called once per frame
    void Update()
    {
        //If required size is moved we repeat track by setting its position to starting
        if (transform.position.z < startPos.z - repeatWidth)
        {
            transform.position = startPos;
            //Generate new part of track after repeat
            GenerateTrack();
        }
    }

    void GenerateTrack(float currentPosition = 0)
    {
        currentPosition = currentPosition == 0 ? repeatWidth : currentPosition;
        //Offset to calculate coordinates from position on track (currentPosition + currentPositionOffset) = coordinate position
        float currentPositionZOffset = transform.position.z - trackLength / 2;
        float currentPositionXOffset = transform.position.x - trackWidth / 2;

        int XPosition, ZPosition, wallHeight;

        //Generate new segment as long as we can
        while (trackLength - currentPosition >= trackSegmentLength)
        {

            //Generate  pickups
            //Pickups clipping is possible, but low probability - out of scope of this assignment
            for(int i = 0; i < pickupsQuantity; i++)
            {
                //Get random position for pickup
                //Last space is required for the wall and there is some blind zone for the player after passing the wall
                ZPosition = Random.Range(1,(int)trackSegmentLength-wallBlindZone-1);
                XPosition = Random.Range(0,(int)trackWidth);

                //Create pickup in coordinates.
                //X is calculated as position on segment + offset of segment + offset due to cube size
                //Z is calculated as current segment position + position on segment + offset of segment + offset due to cube size
                Instantiate(pickupPrefab, new Vector3(XPosition + currentPositionXOffset + cubeSizeOffset, 0, currentPosition + ZPosition + currentPositionZOffset + cubeSizeOffset), Quaternion.identity);
            }

            //Generate wall
            for (int i = 0; i < (int)trackWidth; i++)
            {
                //Get random wall height for this segment of the wall in cubes
                wallHeight = Random.Range(1,wallMaxSize+1);
                for(int j = 1; j <= wallHeight; j++)
                {
                    //Create wall cube in coordinates
                    //X is calculated as position of wall segment + offset of track segment + offset due to cube size
                    //Y is calculated as current cube in wall segment decreased by to transition to coordinates
                    //Z is calculated as current segment position + last position on segment + offset of segment + offset due to cube size
                    Instantiate(wallPrefab, new Vector3((float)i + currentPositionXOffset + cubeSizeOffset, j-1, currentPosition + (trackSegmentLength - 1) + currentPositionZOffset + cubeSizeOffset), Quaternion.identity);
                }


            }

            //Going to new segment
            currentPosition += trackSegmentLength;
        }
    }
}
