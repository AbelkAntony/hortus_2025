using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance{ get; private set; }

    public TextMeshProUGUI TotalHieghtText;

    public List<TextMeshProUGUI> LeaderBoardText;

    public GameObject MainMenuPanel;
    public  GameObject GameOverPanel;
    public GameObject LeaderBoardPanel;

    public TextMeshProUGUI NameInput;

    public TextMeshProUGUI TimerText;

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

    public void UpdateTotalHeight(float height)
    {
        TotalHieghtText.text = $"{height}m"; // Format the height to 2 decimal places and append "m" for meters.
    }

    public void ShowLeaderBoardText()
    {
        GameManager.Instance.leaderBoard.Show();
        var scores = GameManager.Instance.leaderBoard._scoreData.scores;
        // arrange scores with highest score first
        scores.Sort((a, b) => b.value.CompareTo(a.value));
        for (int i = 0; i < LeaderBoardText.Count; i++)
        {
            if (i < scores.Count)
            {
                if(scores[i].name.Length >= 3) // Check if the name has at least 3 characters.
                    LeaderBoardText[i].text = $"{scores[i].name[0]}{scores[i].name[1]}{scores[i].name[2]} - {scores[i].value}m";
                else
                    LeaderBoardText[i].text = $"{scores[i].name} - {scores[i].value}m"; // Display the full name if it's less than 3 characters.
            }
            else
            {
                LeaderBoardText[i].text = "";
            }
        }
    }

    internal void ShowGameOverScreen()
    {
        GameOverPanel.SetActive(true);
    }

    internal void ShowLeaderBoard()
    {
         if(LeaderBoardPanel.activeSelf)
        {
            LeaderBoardPanel.SetActive(false); // Deactivate the leaderboard panel.
            GameOverPanel.SetActive(false); // Deactivate the game over panel.
            MainMenuPanel.SetActive(true); // Activate the main menu panel.
            return;
        }
        
        LeaderBoardPanel.SetActive(true); // Activate the leaderboard panel.
        MainMenuPanel.SetActive(false);
        ShowLeaderBoardText();
    }

    public void ShowMainMenu()
    {

        MainMenuPanel.SetActive(true);
        LeaderBoardPanel.SetActive(false);
        GameOverPanel.SetActive(false);
    }

    internal void UpdateTimer(float currentTime)
    {
        TimerText.text = "Time: " + Mathf.Round(currentTime).ToString(); // Update the timer text with the current time.
    }

    public void GameStart()
    {
        if (MainMenuPanel.activeSelf)
        {
            MainMenuPanel.SetActive(false); // Deactivate the main menu panel.
            LeaderBoardPanel.SetActive(false); // Deactivate the leaderboard panel.
            GameOverPanel.SetActive(false); // Deactivate the game over panel.
        }

    }
}
