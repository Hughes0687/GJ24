using UnityEngine;
using System.Collections;

public class ParticleToggle : MonoBehaviour
{
    private ParticleSystem particleSystem;
    private Coroutine stopCoroutine;

    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();

        if (particleSystem.isPlaying)
        {
            particleSystem.Stop();
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Player.instance.fuel > 0)
        {
            if (stopCoroutine != null)
            {
                StopCoroutine(stopCoroutine);
                stopCoroutine = null;
            }

            if (!particleSystem.isPlaying)
            {
                particleSystem.Play();
            }
        }
        else
        {
            if (stopCoroutine == null && particleSystem.isPlaying)
            {
                stopCoroutine = StartCoroutine(StopParticleSystemAfterDelay());
            }
        }
    }

    private IEnumerator StopParticleSystemAfterDelay()
    {
        yield return new WaitForSeconds(1.0f);
        particleSystem.Stop();
        stopCoroutine = null;
    }
}