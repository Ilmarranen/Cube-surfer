using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] Animator animator;
    [SerializeField] private float speed = 20;
    [SerializeField] private float horizontalLimit = 2;
    private float horizontalValue;
    private float newPositionX;

    private void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //If game started and there was no game over
        if(gameManager.isActive)
        {
            //If touching we'll get x axis of position touched
            if (Input.GetMouseButton(0))
            {
                horizontalValue = Input.GetAxis("Mouse X");
            }
            else
            {
                horizontalValue = 0;
            }

            //New position on x axis according to speed and touched offset
            newPositionX = transform.position.x + horizontalValue * speed * Time.deltaTime;
            //Clamping position to track horizontal limits
            newPositionX = Mathf.Clamp(newPositionX, -horizontalLimit, horizontalLimit);
            //Updating player position (horizontal movement)
            transform.position = new Vector3(newPositionX, transform.position.y, transform.position.z);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CubeWall"))
        {
            gameManager.GameOver();
        }
  
    }

    //Play jump animation
    public void Jump()
    {
        animator.SetBool("Jump", true);
    }
}
