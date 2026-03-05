using System.Collections;
using UnityEngine;

public class TimerToRestartLevel : MonoBehaviour
{
    private IEnumerator timer;

    private void Start()
    {
        EventManager.Instance.PlayerDeath += CallStartTimer;
    }

    public void CallStartTimer()
    {
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
        for (int tenthSecond = 0; tenthSecond <= 10; tenthSecond++) {
            yield return new WaitForSeconds(0.1f);
        }
        EventManager.Instance.StartLevelFunc();
        StopTimer();
    }

    private void OnDestroy()
    {        
        EventManager.Instance.PlayerDeath -= CallStartTimer;
    }
}
