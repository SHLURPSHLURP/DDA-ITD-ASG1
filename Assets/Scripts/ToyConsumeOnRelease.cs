using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ToyConsumeOnRelease : MonoBehaviour
{
    public float consumeDistance = 0.15f;

    Transform snapZone;
    UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grab;

    void Awake()
    {
        grab = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

        GameObject snapObj = GameObject.Find("SnapZone");
        if (snapObj != null)
            snapZone = snapObj.transform;

        grab.selectExited.AddListener(OnReleased);
    }

    void OnReleased(SelectExitEventArgs args)
    {
        if (snapZone == null) return;

        float dist = Vector3.Distance(transform.position, snapZone.position);

        if (dist <= consumeDistance)
        {
            Play();
        }
    }

    void Play()
    {
        Debug.Log("Toy used!");

        GameState.Instance.toy--;
        GameState.Instance.score += 1;   // 🎉 play increases score

        PetSpinReaction pet = GameObject.FindObjectOfType<PetSpinReaction>();
        if (pet != null)
            pet.Spin();

        Destroy(gameObject);
    }
}
