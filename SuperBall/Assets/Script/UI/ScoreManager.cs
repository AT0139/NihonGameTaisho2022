using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private ScoreChange ScoreChange;

    // 現在のスコア
    private int m_Score;

    void Start()
    {
        m_Score = 0;

        ScoreChange.ShowScore(m_Score);
    }

    public void AddScore(int score)
    {
        m_Score += score;

        ScoreChange.ShowScore(m_Score);
    }
}
