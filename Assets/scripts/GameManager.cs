using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance {get; private set;} 
    public bool isGameOver;
    public bool isGameSessionActive;

    public float TotalHeight;

    //private Vector3 previousPlayerPosition;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartGame()
    {
        TotalHeight = 0;
        UIManager.Instance.UpdateTotalHeight(0);
        isGameOver = false;
        isGameSessionActive = true;
    }

    internal void GameOver()
    {
        isGameOver = true;
    }

    public void IncreaseHeight()
    {
        TotalHeight += 10;
        UIManager.Instance.UpdateTotalHeight(TotalHeight);
    }
}

