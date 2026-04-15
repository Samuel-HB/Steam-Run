using System.Collections;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TMP_Text timerText;
    private IEnumerator timer;
    private int second = 0;
    private int minute = 0;
    private int hour = 0;
    private bool showMinute = false;
    private bool showHour = false;
    private Color timerColor;


    private void Start()
    {
        EventManager.Instance.AbleToPlay += CallStartTimer;
        EventManager.Instance.PlayerDeath += StopTimer;
        EventManager.Instance.EndLevelReached += StopTimer;
        EventManager.Instance.EndLevelReached += HideTimer;

        timerColor = timerText.color;
        ResetTimer();
    }

    public void CallStartTimer()
    {
        minute = 0;
        hour = 0;
        showMinute = false;
        showHour = false;

        timer = StartTimer();
        StartCoroutine(timer);
    }

    public void StopTimer()
    {
        if (timer != null)
        {
            StopCoroutine(timer);
            timer = null;
        }
        ResetTimer();
    }

    private void ResetTimer()
    {
        timerText.text = "00";
    }

    private void HideTimer()
    {
        timerColor.a = 0f;
        timerText.color = timerColor;
    }

    IEnumerator StartTimer()
    {
        for (second = 0; second <= 61; second++)
        {
            if (second >= 60)
            {
                minute++;
                second = 0;
                showMinute = true;
            }
            if (minute >= 60)
            {
                hour++;
                minute = 0;
                showMinute = false;
                showHour = true;
            }

            if (showHour) {
                timerText.text = hour.ToString("") + ":" + minute.ToString("00") + ":" + second.ToString("00");
            }
            else if (showMinute) {
                timerText.text = minute.ToString("") + ":" + second.ToString("00");
            }
            else {
                timerText.text = second.ToString("00");
            }

            yield return new WaitForSeconds(1);
        }
    }

    private void OnDestroy()
    {
        EventManager.Instance.AbleToPlay -= CallStartTimer;
        EventManager.Instance.PlayerDeath -= StopTimer;
        EventManager.Instance.EndLevelReached -= StopTimer;
        EventManager.Instance.EndLevelReached -= HideTimer;
    }
}
