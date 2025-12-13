/// 
/// Script to eat the food
/// Made by Gracie Arianne Peh (S10265899G) 12/12/25
/// chatgpt reference
/// 

using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FoodConsumeOnRelease : MonoBehaviour
{
    public float consumeDistance = 0.15f;

    Transform snapZone;
    UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grab;

    void Awake()
    {
        grab = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

        // Find SnapZone in the scene at runtime
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
            Eat();
        }
    }

    void Eat()
    {
        Debug.Log("Food eaten");

        // Update game data
        GameState.Instance.food--;

        // Trigger pet reaction (spin)
        PetSpinReaction pet = GameObject.FindObjectOfType<PetSpinReaction>();
        if (pet != null)
        {
            Debug.Log("PetSpinReaction found");
            pet.Spin();
        }
        else
        {
            Debug.Log("PetSpinReaction NOT found");
        }


        // Remove the food
        Destroy(gameObject);
    }
}
