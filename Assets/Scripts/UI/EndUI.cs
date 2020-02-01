using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndUI : MonoBehaviour
{
    [SerializeField]
    private GameObject winLogo = null;
    [SerializeField]
    private Image winnerImg = null;
    [SerializeField]
    private Sprite redGroupSprite = null;
    [SerializeField]
    private Sprite blueGroupSprite = null;
    [SerializeField]
    private Sprite tieSprite = null;

    public void ShowWinner(GroupType winner)
    {
        winLogo.SetActive(true);
        winnerImg.sprite = winner == GroupType.Blue ? blueGroupSprite : redGroupSprite;
        gameObject.SetActive(true);
    }

    public void ShowTie()
    {
        winLogo.SetActive(false);
        winnerImg.sprite = tieSprite;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
