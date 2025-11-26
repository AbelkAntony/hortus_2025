using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner Instance { get; private set; } // Singleton pattern to access the spawner from anywhere in the game

    public Building BuildingPrefab;
    public Building NewBuilding;
    public Building FallingBuilding;
    public Building TopBuilding; 
    public List<Building> AllBuildings;

    public float SpawnDelay;
    public float BuildingWidth;
    public float BuildingHeight;
    public float MoveHeight;
    public float MoveSpeed;
    public float MoveOffset;
    public int direction = 1;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        
    }

    void Start()
    {
        AllBuildings = new();
        SpawnBuilding();
    }

    void Update()
    {
        if(!GameManager.Instance.isGameSessionActive || GameManager.Instance.isGameOver)
            return;

        MoveSpawner();

        if (Input.GetKeyDown(KeyCode.Space) && NewBuilding!= null)
        {
            FallBuilding();       
            SpawnNextBuilding();
        }

    }

    private void SpawnNextBuilding()
    {
        // delay the spawning so that the falling building will not collide.
        StartCoroutine(WaitSpawnBuilding());
    }

    private IEnumerator WaitSpawnBuilding()
    {
        yield return new WaitForSeconds(SpawnDelay);
        SpawnBuilding(); //call the spawn method from building script
    }

    private void SpawnBuilding()
    {
       NewBuilding = Instantiate(BuildingPrefab, transform.position, Quaternion.identity);
       NewBuilding.transform.parent = transform; //set the spawner as parent of building
    }

    // set the building to fall
    private void  FallBuilding()
    {
        // Sepearate out the falling building so that it doesnt interfere with the spawing
        FallingBuilding = NewBuilding;
        NewBuilding = null;
        FallingBuilding.transform.parent = null;
        FallingBuilding.SetFalling(); //call the fall method from building script
    }

    private void MoveSpawner()
    {
        //Spawner move back and forth from -offset to offset
        if (direction == -1 && transform.position.x <= -MoveOffset)
        {
            direction = 1;      
        }
        else if (transform.position.x >= MoveOffset)
        {
            direction = -1;
        }
        
        transform.position += new Vector3(direction* MoveSpeed * Time.deltaTime, 0, 0);
    }

    internal void Hit()
    {

        float difference = Mathf.Abs(TopBuilding.transform.position.x - FallingBuilding.transform.position.x);
        //Debug.Log($"{difference/BuildingWidth} : {difference}");

        // Check for accuracy
        if(difference/BuildingWidth > 0.8f)
        {
            Debug.Log("fail");
            FallingBuilding.GetComponent<Rigidbody>().useGravity = true;
            GameManager.Instance.GameOver();
            return;
        }
        else if(difference/BuildingWidth < 0.1f)
        {
            Debug.Log("prefect");
            // adjusting x and y position to fit perfectly
            FallingBuilding.transform.position = new Vector3(TopBuilding.transform.position.x, TopBuilding.transform.position.y + BuildingHeight , FallingBuilding.transform.position.z);
        }
        else
        {
            // adjust y to fit correctly on top
            FallingBuilding.transform.position = new Vector3(FallingBuilding.transform.position.x, TopBuilding.transform.position.y + BuildingHeight , FallingBuilding.transform.position.z);
            Debug.Log("avg");
        }

        FallingBuilding.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        TopBuilding = FallingBuilding;
        // disabling for performance and to prevent collision detection with itself and other buildings.
        FallingBuilding.enabled = false;
        AllBuildings.Add(TopBuilding);

        SetNextYPosition();
        GameManager.Instance.IncreaseHeight();
    }

    // set the positon of Y for this object
    public void SetNextYPosition()
    {
        transform.position = new Vector3(transform.position.x, TopBuilding.transform.position.y + MoveHeight, transform.position.z); //move up when space is pressed
    }
}
