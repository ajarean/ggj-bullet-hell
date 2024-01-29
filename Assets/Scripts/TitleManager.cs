using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI highScoreText;
    [SerializeField] TextMeshProUGUI deathsText;
    public void StartButton(){
        SceneManager.LoadScene("TingExampleUI");
    }

    void Update()
    {
        highScoreText.text = ResultsManager.GetHighScore() + "";
        deathsText.text = ResultsManager.GetDeaths() + "";
    }
}
