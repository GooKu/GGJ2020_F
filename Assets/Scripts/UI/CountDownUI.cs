using UnityEngine;
using UnityEngine.UI;
using System;

public class CountDownUI : MonoBehaviour
{
    public event Action OnCountDownFinishEvent;

    [SerializeField]
    private Text countDownText;
    [SerializeField]
    private float timeLimit;

    private float time = 0;
    private bool isStart = false;

    public void Init()
    {
        time = timeLimit;
    }

    public void StartCountDown()
    {
        isStart = true;
    }

    private void FixedUpdate()
    {
        countDownText.text = time.ToString("00.00");
    }

    private void Update()
    {
        if (isStart)
        {
            time -= Time.deltaTime;

            if(time <= 0)
            {
                time = 0;
                isStart = false;
                OnCountDownFinishEvent?.Invoke();
            }
        }
    }
}
