using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] CameraManger cam;
    [SerializeField] GameObject Player;
    private GameObject newPlayer;
    private float distanceBetweenPlayers;
    private Vector3 previousPlayerPosition;

    //private Vector3 previousPlayerPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartGame()
    {
        Vector3 PlayerPosition = new Vector3(0, 2, 0);
       // cam.CameraHeight(PlayerPosition.y);
        Instantiate(Player,PlayerPosition, Quaternion.identity);
        cam.CameraHeight(PlayerPosition.y);
    }

    public void SpwanPlayer(Vector3 previousPlayerPos)
    {
        previousPlayerPosition = previousPlayerPos;
        previousPlayerPosition.y += 4;
        cam.CameraHeight(previousPlayerPosition.y);
        GameObject newPlayer = Instantiate(Player, previousPlayerPosition, Quaternion.identity);
    }

    public void GameOver()
    {
        Invoke("ResetScene", 0.5f);

    }

    private void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

