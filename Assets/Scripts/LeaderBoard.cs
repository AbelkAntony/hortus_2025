
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public struct Score
{
    public string name;
    public int value;
}

[Serializable]
public class ScoreListWrapper
{
    public List<Score> scores = new List<Score>();
}

public class LeaderBoard
{
    private const string ScoreKey = "scoreList";
    public ScoreListWrapper _scoreData = new ScoreListWrapper();


    public LeaderBoard()
    {
        GetSavedScores();
    }

    public void Show()
    {
        GetSavedScores();
    }

    private void GetSavedScores()
    {
        //scores = new();
        string json = PlayerPrefs.GetString(ScoreKey, string.Empty);
        if (!string.IsNullOrEmpty(json))
        {
            _scoreData = JsonUtility.FromJson<ScoreListWrapper>(json);
        }
        else
        {
            _scoreData = new ScoreListWrapper();
        }
    }

    public void SaveNewScore(Score score)
    {
        GetSavedScores();
         _scoreData.scores.Add(score);

        //Debug.Log(score.Name);
        //Debug.Log(scores.ToArray());
        // string jsonScorelist = JsonUtility.ToJson(scores); 
        // PlayerPrefs.SetString("scoreList", jsonScorelist); // Saves the JSON string to PlayerPrefs
        // Debug.Log(jsonScorelist);
        string json = JsonUtility.ToJson(_scoreData);
        PlayerPrefs.SetString(ScoreKey, json);
        PlayerPrefs.Save();
    }
}
