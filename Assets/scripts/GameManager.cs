using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance {get; private set;} 
    public LeaderBoard leaderBoard; 

    public bool isGameOver;
    public bool isGameSessionActive;

    public int TotalHeight;

    //private Vector3 previousPlayerPosition;

    public float TotalPlayTime;
    public bool isTimeStarted;
    public float CurrentTime;
    private bool isFirstHit;

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
        leaderBoard = new();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SceneManager.LoadScene(1,LoadSceneMode.Additive);
        RestartGame();
    }

    // Update is called once per frame
    void Update()
    {
        if(isTimeStarted)
        {
            RunTimer();
        }

        if(isGameSessionActive == false && isGameOver == false)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                StartGame();
            }

            if(Input.GetKeyDown(KeyCode.Tab))
            {
                UIManager.Instance.ShowLeaderBoard();
            }
            return;
        }
        else if( isGameSessionActive)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                Spawner.Instance.HitFall();
            }
        }
        
        if(isGameOver == true)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                Score newScore = new()
                {
                    name = UIManager.Instance.NameInput.text,
                    value = TotalHeight
                };

                leaderBoard.SaveNewScore(newScore);
                reloadGame();
            }
            return;
        }
        
        
        if(isGameSessionActive && !isTimeStarted)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                isTimeStarted = true;
            }
        }   
    }

    private void RunTimer()
    {
        CurrentTime -= Time.deltaTime; // Decrease current time by the time elapsed since last frame

        if(CurrentTime <= 0)
            GameOver();

        UIManager.Instance.UpdateTimer(CurrentTime); // Update the timer display in the UI
    }

private void reloadGame()
    {
        SceneManager.LoadScene(0);
    }
    private void RestartGame()
    {

        
        Spawner.Instance.Restart(); // Restart the spawner
        UIManager.Instance.ShowMainMenu(); 
        isGameSessionActive = false; // Set game session active to false
        isGameOver = false;
        isTimeStarted = false; // Reset time started flag
        CurrentTime = TotalPlayTime; // Reset current time to total play time
        UIManager.Instance.UpdateTimer(CurrentTime); // Update timer UI
    }

    public void StartGame()
    {
        TotalHeight = 0;
        UIManager.Instance.UpdateTotalHeight(0);
        isGameOver = false;
        isGameSessionActive = true;
        isTimeStarted = false; // Reset time started flag
        CurrentTime = TotalPlayTime;
        UIManager.Instance.GameStart();
    }

    internal void GameOver()
    {        
        isTimeStarted = false;
        isGameOver = true;
        UIManager.Instance.ShowGameOverScreen(); // Show the game over screens
    }

    public void IncreaseHeight()
    {
        TotalHeight += 10;
        UIManager.Instance.UpdateTotalHeight(TotalHeight);
    }
}

