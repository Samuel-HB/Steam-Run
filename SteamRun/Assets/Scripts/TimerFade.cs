using System.Collections;
using UnityEngine;
using TMPro;

public class TimerFade : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    private Color color;

    private void Start()
    {
        EventManager.Instance.RestartLevel += InvisibleText;
        EventManager.Instance.AbleToPlay += Fade;

        color = text.color;
        InvisibleText();
    }

    private void Fade()
    {
        StartCoroutine(Fader(0.25f));
    }

    private void InvisibleText()
    {
        color.a = 0f;
        text.color = color;
    }

    IEnumerator Fader(float duration)
    {
        float time = 0;
        while (time < duration)
        {
            time += Time.deltaTime;
            color.a = time / duration;
            text.color = color;
            yield return null;
        }
    }

    private void OnDestroy()
    {
        EventManager.Instance.RestartLevel -= InvisibleText;
        EventManager.Instance.AbleToPlay -= Fade;
    }
}
