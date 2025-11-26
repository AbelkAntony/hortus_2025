using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance{ get; private set; }

    public TextMeshProUGUI TotalHieghtText;

    public List<TextMeshProUGUI> LeaderBoardText;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
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

    public void ShowLeaderBoardText(List<Score> scores)
    {
        // arrange scores with highest score first
        scores.Sort((a, b) => b.height.CompareTo(a.height));
        for (int i = 0; i < LeaderBoardText.Count; i++)
        {
            if (i < scores.Count)
            {
                LeaderBoardText[i].text = $"{scores[i].Name[0]}{scores[i].Name[1]}{scores[i].Name[2]} - {scores[i].height}m";
            }
            else
            {
                LeaderBoardText[i].text = "";
            }
        }
    }

}
