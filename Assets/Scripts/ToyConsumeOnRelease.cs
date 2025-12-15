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

    void OnReleased(SelectExitEventArgs args)
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
