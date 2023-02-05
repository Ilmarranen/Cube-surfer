using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackController : MonoBehaviour
{
    private PlayerController playerController;
    public List<GameObject> cubeList = new List<GameObject>();
    private GameObject lastCube;
    [SerializeField] GameObject collectCubeTextPrefab;
    [SerializeField] Vector3 collectCubeTextOffset = new Vector3(0,3.5f,0);

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindObjectOfType<PlayerController>();
        UpdateLastCube();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Add new cube to the stack
    public void AddCube(GameObject cubeObject)
    {   
        //Move the whole stack up
        transform.position = transform.position + Vector3.up;

        //Position the new cube where last was and include it in player object. It is now the last
        cubeObject.transform.position = lastCube.transform.position + Vector3.down;
        cubeObject.transform.SetParent(transform);
        cubeList.Add(cubeObject);
        UpdateLastCube();

        //Inform player to play jump animation
        playerController.Jump();

        //Create floating text on cube pickup
        Instantiate(collectCubeTextPrefab,transform.position + collectCubeTextOffset, Quaternion.identity,transform);
    }

    //Remove cube from the stack
    public void RemoveCube(GameObject cubeObject)
    {
        cubeObject.transform.parent = null;
        cubeList.Remove(cubeObject);
        UpdateLastCube();
    }

    //Update lowest cube in stack (last added)
    void UpdateLastCube()
    {
        lastCube = cubeList[cubeList.Count - 1];
    }
}
