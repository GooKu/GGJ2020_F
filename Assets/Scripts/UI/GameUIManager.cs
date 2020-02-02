using UnityEngine;
using UnityEngine.UI;
public class GameUIManager : MonoBehaviour
{
    [Header("TeamA分數")]
    public Text TeamAScoreText;
    [Header("TeamB分數")]
    public Text TeamBScoreText;
    int TeamAScore = 0;
    int TeamBScore = 0;
    
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
    public int getTeamBScore()
    {
        return this.TeamBScore;
    }
}
