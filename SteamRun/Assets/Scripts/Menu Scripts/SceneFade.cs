using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SceneFade : MonoBehaviour
{
    [SerializeField] private Image image;

    private void Start()
    {
        EventManager.Instance.PlayerDeath += Fade;
    }

    private void Fade()
    {
        StartCoroutine(Fader(0.25f));
    }

    IEnumerator Fader(float duration)
    {
        float time = 0;
        Color color = image.color;
        while (time < duration)
        {
            time += Time.deltaTime;
            color.a = time / duration;
            image.color = color;
            yield return null;
        }
        StartCoroutine(BlackScreen());
    }

    IEnumerator BlackScreen()
    {
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(FadeOut(0.25f));
    }

    IEnumerator FadeOut(float duration)
    {
        float time = 0;
        Color color = image.color;
        while (time < duration)
        {
            time += Time.deltaTime;
            color.a = 1f - ((time * 4) / 1f);
            image.color = color;
            yield return null;
        }
    }

    private void OnDestroy()
    {
        EventManager.Instance.PlayerDeath -= Fade;
    }
}
