using System.Collections;
using UnityEngine;

public class TimerToBeAbleToPlay : MonoBehaviour
{
    private IEnumerator timer;
    [SerializeField] private int tenthSecondToWaitBeforePlay = 10;

    private void Start()
    {
        EventManager.Instance.RestartLevel += CallStartTimer;
        CallStartTimer();
    }

    public void CallStartTimer()
    {
        Debug.Log("timer");
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
    }

    IEnumerator StartTimer()
    {
        for (int tenthSecond = 0; tenthSecond <= tenthSecondToWaitBeforePlay; tenthSecond++) {
            yield return new WaitForSeconds(0.1f);
        }
        EventManager.Instance.AbleToPlayFunc();
        StopTimer();
    }

    private void OnDestroy()
    {        
        EventManager.Instance.RestartLevel -= CallStartTimer;
    }
}
