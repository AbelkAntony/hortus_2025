using UnityEngine;
using UnityEngine.InputSystem;
public class cube_controller : MonoBehaviour
{
    private GameManager gameManager;
    private GameObject player;
    private bool playerMove;
    private Rigidbody rbPlayer;
    private float playerSpeed = 5f;
    private float playerLeftLimit = -5f;
    private float playerRightLimit = 5f;
    private int playerDirection = 1;
    private bool playerLanded = false;
    private void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        player = GetComponent<GameObject>();
        rbPlayer = GetComponent<Rigidbody>();
        playerMove = true;
    }

    private void FixedUpdate()
    {
        if (playerMove)
        {
            rbPlayer.linearVelocity = new Vector3(playerDirection * playerSpeed, rbPlayer.linearVelocity.y, rbPlayer.linearVelocity.z);

            //left right direction
            if (transform.position.x >= playerRightLimit)
            {
                playerDirection = -1;
            }
            else if (transform.position.x <= playerLeftLimit)
            {
                playerDirection = 1;
            }

        }
        else
        {
            rbPlayer.linearVelocity = new Vector3(0, -playerSpeed, 0);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            playerMove = false;
            

        }
        if (Input.GetKeyDown(KeyCode.RightControl))
        {
            playerMove = true;
        }
        

    }
    private void OnCollisionEnter(Collision collision)
    {
        if(!playerLanded)
        {
            //gameManager.PlayerLanded();
            if(collision.gameObject.tag == "Player")
            {
                Debug.Log(collision.gameObject.tag);
                float distanceBetweenPlayers = Vector3.Distance(this.transform.position, collision.gameObject.transform.position);
                Debug.Log(distanceBetweenPlayers);
                if(distanceBetweenPlayers<1.1)
                {
                    Debug.Log(distanceBetweenPlayers);
                    FreezPlayer();
                }
                else
                {
                    gameManager.GameOver();
                    this.gameObject.tag = "Untagged";
                }
               
            }
            else
            {
                Debug.Log(collision.gameObject.tag);
                FreezPlayer();
            }
        }
        else
        {
            
        }
    }
    private void FreezPlayer()
    {
        gameManager.SpwanPlayer(this.gameObject.transform.position);
        rbPlayer.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
        playerLanded = true;
    }

}
