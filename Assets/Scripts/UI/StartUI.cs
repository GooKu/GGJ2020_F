using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartUI : MonoBehaviour
{
    [SerializeField]
    private Text playerCount = null;
    [SerializeField]
    private Button startBtn = null;

    public void OnPlayerCountChange(int count)
    {
        playerCount.text = $"{count}/6";
        startBtn.interactable = count > 0 && count % 2 == 0;
    }

    public void Show()
    {
        gameObject.SetActive(true);
        playerCount.text = "0/6";
        startBtn.interactable = false;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
