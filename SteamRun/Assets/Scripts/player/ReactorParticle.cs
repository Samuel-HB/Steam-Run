using UnityEngine;
using System.Collections;

public class ReactorParticle : MonoBehaviour
{
    [SerializeField] private ParticleSystem smoke;

    private void Start()
    {
        EventManager.Instance.RestartLevel += DirectlyEndSmoke;

        DirectlyEndSmoke();
    }

    public void DirectlyEndSmoke()
    {
        if (smoke.isPlaying == true) {
            smoke.Stop();
        }
    }

    public void StartSmoke()
    {
        StopAllCoroutines();

        if (smoke.isPlaying == true) {
            smoke.Stop();
        }
        smoke.Play();
    }

    public void EndSmoke()
    {
        StopAllCoroutines();

        StartCoroutine(WaitBeforeStopSmoke());
    }

    IEnumerator WaitBeforeStopSmoke()
    {
        yield return new WaitForSeconds(0.15f);

        if (smoke.isPlaying == true) {
            smoke.Stop();
        }
    }

    private void OnDestroy()
    {
        EventManager.Instance.RestartLevel -= DirectlyEndSmoke;
    }
}
