using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreChange : MonoBehaviour
{
    [SerializeField]
    //private Rawimage ScoreDigit_2; // ２桁目

    //[SerializeField]
    private GameObject ScoreDigit_1; // １桁目

    private int m_Score;

    void Start()
    {
        m_Score = 0;

        ShowScore();
    }

    private void ShowScore()
    {
        
    }

    public void SetScore(int score)
    {
        m_Score += score;
    }
}
