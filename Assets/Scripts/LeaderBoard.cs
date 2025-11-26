
using System.Collections.Generic;
using UnityEngine;


public struct Score
{
    public string Name;
    public int height;
}

public class LeaderBoard
{
    public List<Score> scores;


    public void Show()
    {
        GetSavedScores();
        ArrangeScoresInOrder();
    }

    private void ArrangeScoresInOrder()
    {

    }

    private void SetUI()
    {
        UIManager.Instance.ShowLeaderBoardText(scores);
    }

    private void GetSavedScores()
    {
        scores = new();
        string jsonScorelist = PlayerPrefs.GetString("scoreList", "");

        if (jsonScorelist.Length > 0)
        {
            scores = JsonUtility.FromJson<List<Score>>(jsonScorelist);
        }
        
    }

    public void SaveNewScore(Score score)
    {
        scores.Add(score);

        string jsonScorelist = JsonUtility.ToJson(scores); // Converts the list of scores to a JSON string
        PlayerPrefs.SetString("scoreList", jsonScorelist); // Saves the JSON string to PlayerPrefs
    }
}
