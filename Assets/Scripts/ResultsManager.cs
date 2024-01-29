using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultsManager : MonoBehaviour
{
    private const string HighScoreKey = "HighScore";
    private const string DeathsKey = "Deaths";

    // Save the high score
    public static void SaveHighScore(int score)
    {
        PlayerPrefs.SetInt(HighScoreKey, score);
        PlayerPrefs.Save();
    }

    public static void AddDeath()
    {
        int deaths = GetDeaths()+1;
        PlayerPrefs.SetInt(DeathsKey, deaths);
        PlayerPrefs.Save();
    }

    // Retrieve the high score
    public static int GetHighScore()
    {
        return PlayerPrefs.GetInt(HighScoreKey, 0);
    }

    public static int GetDeaths()
    {
        return PlayerPrefs.GetInt(DeathsKey, 0);
    }
}
