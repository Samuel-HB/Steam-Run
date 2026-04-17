using UnityEngine;
using System.Collections;

public class ReactorParticle : MonoBehaviour
{
    [SerializeField] private ParticleSystem smoke;

    private void Start()
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
}
