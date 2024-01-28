using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreManager : MonoBehaviour
{
    // Start is called before the first frame update

    public int score = 0;
    public int hiScore = 0;
    [SerializeField] TextMeshProUGUI scoreText; //I LOVE VIOLATING EVERY SOLID PRINCIPLE IN MY CODE
    private float timer = 0;

    private bool playerNotDead = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerNotDead){
            timer += Time.deltaTime;
            if(timer%60 >= 1){
                score += 5;
                timer = 0;
            }
            scoreText.text = score.ToString();
        }
        else{ //you are dead
            if(score > hiScore){
                hiScore = score;
            }
            score = 0;
            timer = 0;
        }
    }
}
