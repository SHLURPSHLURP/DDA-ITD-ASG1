/// 
/// small script to be able to link audio to spawned prefabs as im unable to link from scene
/// Made by Gracie Arianne Peh (S10265899G) 15/12/25
/// ChatGPT generated
/// 

using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabAudioHook : MonoBehaviour
{
    UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grab;

    void Awake()
    {
        grab = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

        grab.selectEntered.AddListener(OnGrab);
        grab.selectExited.AddListener(OnRelease);
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayPickup();
    }

    void OnRelease(SelectExitEventArgs args)
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayPutdown();
    }
}
