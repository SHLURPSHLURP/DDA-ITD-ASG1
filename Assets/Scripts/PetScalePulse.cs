/// 
/// Script for pet to pulse when mood is logged
/// Made by Gracie Arianne Peh (S10265899G) 12/12/25
/// chatgpt reference
/// 

using UnityEngine;
using System.Collections;

public class PetScalePulse : MonoBehaviour
{
    public float pulseScale = 1.05f;   
    public float pulseDuration = 0.25f;
    public float pauseBetweenPulses = 0.08f;

    private Vector3 originalScale;
    private bool isPulsing = false;

    void Awake()
    {
        originalScale = transform.localScale;
    }

    public void PlayPulse() //TO CALL FROM OTHER SCRIPTS/OBJECTS
    {
        if (!isPulsing)
            StartCoroutine(DoublePulseRoutine());
    }

    IEnumerator DoublePulseRoutine()
    {
        isPulsing = true;

        // Pulse 1
        yield return StartCoroutine(PulseOnce());

        yield return new WaitForSeconds(pauseBetweenPulses);

        // Pulse 2
        yield return StartCoroutine(PulseOnce());

        transform.localScale = originalScale;
        isPulsing = false;
    }

    IEnumerator PulseOnce()
    {
        float half = pulseDuration / 2f;
        float t = 0f;

        // Scale UP
        while (t < half)
        {
            float lerp = t / half;
            transform.localScale =
                Vector3.Lerp(originalScale, originalScale * pulseScale, lerp);
            t += Time.deltaTime;
            yield return null;
        }

        t = 0f;

        // Scale DOWN
        while (t < half)
        {
            float lerp = t / half;
            transform.localScale =
                Vector3.Lerp(originalScale * pulseScale, originalScale, lerp);
            t += Time.deltaTime;
            yield return null;
        }
    }
}
