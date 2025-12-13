/// 
/// Tiny script to spawn draggable clone from "dispenser" food (inspired by pikmin bloom!!!)
/// Made by Gracie Arianne Peh (S10265899G) 12/12/25
/// chatgpt reference
/// 



using UnityEngine;

public class FoodDispenser : MonoBehaviour
{
    public GameObject foodClonePrefab;

    public void SpawnFood()
    {
        if (GameState.Instance.food <= 0) return;

        Instantiate(
            foodClonePrefab,
            transform.position + Vector3.up * 0.05f,
            Quaternion.identity
        );
    }
}

