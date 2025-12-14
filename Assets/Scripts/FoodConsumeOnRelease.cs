using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FoodConsumeOnRelease : MonoBehaviour
{
    public float consumeDistance = 0.15f;

    [Header("Linked via Inspector")]
    public Transform snapZone;
    public PetSpinReaction petSpin;

    UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grab;

    void Awake()
    {
        grab = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        grab.selectExited.AddListener(OnReleased);
    }

    void OnDestroy()
    {
        if (grab != null)
            grab.selectExited.RemoveListener(OnReleased);
    }

    void OnReleased(SelectExitEventArgs args)
    {
        if (snapZone == null || petSpin == null)
            return;

        if (Vector3.Distance(transform.position, snapZone.position) <= consumeDistance)
            Eat();
    }

    void Eat()
    {
        Debug.Log("Food eaten");

        petSpin.Spin();
        Destroy(gameObject);
    }
}
