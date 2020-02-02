using UnityEngine;
using UnityEngine.UI;
public class GameUIManager : MonoBehaviour
{
    [Header("TeamA分數")]
    public ScoreText TeamAScoreTextObj;
    [Header("TeamB分數")]
    public ScoreText TeamBScoreTextObj;
    int TeamAScore = 0;
    int TeamBScore = 0;
    
    public void setTeamAScore(int score)
    {
        this.TeamAScore = score;
        TeamAScoreTextObj.setScore(score);
    }
    public int getTeamAScore()
    {
        return this.TeamAScore;
    }
    public void setTeamBScore(int score)
    {
        this.TeamBScore = score;
        TeamBScoreTextObj.setScore(score);
    }
    public int getTeamBScore()
    {
        return this.TeamBScore;
    }
}
