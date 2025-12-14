using UnityEngine;

public class FoodDispenser : MonoBehaviour
{
    public GameObject foodClonePrefab;

    [Header("Pet References (drag from PetCard)")]
    public Transform snapZone;
    public PetSpinReaction petSpin;

    public void SpawnFood()
    {
        if (GameState.Instance.food <= 0)
            return;

        GameObject food = Instantiate(
            foodClonePrefab,
            transform.position + Vector3.up * 0.05f,
            Quaternion.identity
        );

        FoodConsumeOnRelease consume = food.GetComponent<FoodConsumeOnRelease>();
        consume.snapZone = snapZone;
        consume.petSpin = petSpin;

        
    }
}
