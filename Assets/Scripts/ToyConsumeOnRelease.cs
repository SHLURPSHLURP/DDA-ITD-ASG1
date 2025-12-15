/// 
/// script to handle the cloned toys (be able to grab) and what to do when it reaches pet
/// Made by Gracie Arianne Peh (S10265899G) 10/12/25 
/// DUPE OF FOOD COUNTERPART WITH CHANGES FOR TOY
/// 

using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ToyConsumeOnRelease : MonoBehaviour
{
    public float consumeDistance = 0.15f;

    [Header("Linked in Inspector")]
    public Transform snapZone;
    public PetSpinReaction petSpin;

    UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grab;

    void Awake()
    {
        grab = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        grab.selectExited.AddListener(OnReleased);
    }

    void OnReleased(SelectExitEventArgs args) //WHAT TO DO WHEN THE FOOD REACHES SNAPZONE
    {
        if (Vector3.Distance(transform.position, snapZone.position) <= consumeDistance)
        {
            GameState.Instance.PlayWithPet();
            AudioManager.Instance.PlayDing();
            petSpin.Spin();
            Destroy(gameObject);
        }
    }
}
