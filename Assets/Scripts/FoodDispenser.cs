/// 
/// script to control the food dispenser (to spawn cloned food)
/// Made by Gracie Arianne Peh (S10265899G) 11/12/25
/// 

using UnityEngine;

public class FoodDispenser : MonoBehaviour
{
    public GameObject foodClonePrefab;

    //PET REFERENCES FOR LOCATION OF WHERE FOOD SHOULD BE DETECTED AND WHAT ANIMATION PET CARRIES OUT
    public Transform snapZone;
    public PetSpinReaction petSpin;

    public void SpawnFood() //SPAWN FOOD CLONE THEN CALL FOODCONSUMEONRELEASE AND CONTROL PET REACTION
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
