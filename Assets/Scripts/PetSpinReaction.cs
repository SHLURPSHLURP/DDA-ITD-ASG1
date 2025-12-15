/// 
/// Script for pet to spin when fed or play
/// Made by Gracie Arianne Peh (S10265899G) 12/12/25
/// chatgpt reference
/// 

using UnityEngine;
using System.Collections;

public class PetSpinReaction : MonoBehaviour
{
    public Transform petVisual;
    public PetLean petLean;

    public void Spin()
    {
        StartCoroutine(SpinRoutine());
    }

    IEnumerator SpinRoutine()
    {
        if (petLean != null)
            petLean.enabled = false;   // STOP lean during spin

        float duration = 0.5f;
        float speed = 720f;

        float t = 0f;
        while (t < duration)
        {
            petVisual.Rotate(Vector3.up, speed * Time.deltaTime);
            t += Time.deltaTime;
            yield return null;
        }

        if (petLean != null)
            petLean.enabled = true;    // Resume lean
    }
}

