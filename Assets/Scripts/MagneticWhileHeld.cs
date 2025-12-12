using UnityEngine;


public class MagneticWhileHeld : MonoBehaviour
{
    public float snapStrength = 4f;
    public float snapRange = 0.25f;

    Transform snapZone;
    UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grab;

    void Awake()
    {
        grab = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        GameObject snapObj = GameObject.Find("SnapZone");
        if (snapObj != null)
            snapZone = snapObj.transform;
    }

    void Update()
    {
        if (!grab.isSelected || snapZone == null) return;

        float dist = Vector3.Distance(transform.position, snapZone.position);

        if (dist < snapRange)
        {
            transform.position = Vector3.Lerp(
                transform.position,
                snapZone.position,
                Time.deltaTime * snapStrength
            );
        }
    }
}
