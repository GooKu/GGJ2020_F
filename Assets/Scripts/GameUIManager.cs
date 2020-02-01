using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameUIManager : MonoBehaviour
{
    public Text TotalTimeText;
    [Header("TeamA分數")]
    public Text TeamAScoreText;
    [Header("TeamB分數")]
    public Text TeamBScoreText;
    int TeamAScore = 0;
    int TeamBScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setTotalTime(float time)
    { 
        TotalTimeText.text = time.ToString();
    }
    
    public void setTeamAScore(int score)
    {
        this.TeamAScore = score;
        TeamAScoreText.text = score.ToString();
    }
    public int getTeamAScore()
    {
        return this.TeamAScore;
    }
    public void setTeamBScore(int score)
    {
        this.TeamBScore = score;
        TeamBScoreText.text = score.ToString();
    }
    public int getTeambScore()
    {
        return this.TeamBScore;
    }

}
