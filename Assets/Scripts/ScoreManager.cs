using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private int totalScore = 0;
    [SerializeField] TMP_Text scoreText; 
    
    public void AddScore(int score)
    {
        totalScore += score;
        scoreText.text = "Score: " + totalScore;
    }
    
}
