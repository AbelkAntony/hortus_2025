using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

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
        MoveSpawner();

        if (Input.GetKeyDown(KeyCode.Space) && NewBuilding!= null)
        {
            FallBuilding();
            transform.position = new Vector3(transform.position.x, transform.position.y + MoveHeight, transform.position.z); //move up when space is pressed
            SpawnNextBuilding();
        }

    }

    private void SpawnNextBuilding()
    {
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

    private void  FallBuilding()
    {
        FallingBuilding = NewBuilding;
        NewBuilding = null;
        FallingBuilding.transform.parent = null;
        FallingBuilding.SetFalling(); //call the fall method from building script
    }

    private void MoveSpawner()
    {
        //Spawner move back and forth from -offset to offset
        if (direction == -1)
        {
            if (transform.position.x <= -MoveOffset)
            {
                direction = 1;
            }
        }
        else
        {
            if (transform.position.x >= MoveOffset)
            {
                direction = -1;
            }
        }
        

        transform.position += new Vector3(direction* MoveSpeed * Time.deltaTime, 0, 0);
    }

    internal void Hit()
    {

        float difference = Mathf.Abs(TopBuilding.transform.position.x - FallingBuilding.transform.position.x);
        Debug.Log($"{difference/BuildingWidth} : {difference}");
        if(difference/BuildingWidth > 0.8f)
        {
            Debug.Log("fail");
            FallingBuilding.GetComponent<Rigidbody>().useGravity = true;
            //game over
        }
        else if(difference/BuildingWidth < 0.1f)
        {
            Debug.Log("prefect");
            FallingBuilding.transform.position = new Vector3(TopBuilding.transform.position.x, FallingBuilding.transform.position.y, FallingBuilding.transform.position.z);
        }
        else
        {
            Debug.Log("avg");
        }
        FallingBuilding.GetComponent<Rigidbody>().isKinematic = false;
        FallingBuilding.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        TopBuilding = FallingBuilding;
        FallingBuilding.enabled = false;
        AllBuildings.Add(TopBuilding);
        //SpawnBuilding();
    }
}
