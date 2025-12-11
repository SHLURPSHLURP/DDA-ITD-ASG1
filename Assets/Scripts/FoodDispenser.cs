/// 
/// Tiny script to spawn draggable clone from "dispenser" food (inspired by pikmin bloom!!!)
/// Made by Gracie Arianne Peh (S10265899G) 11/12/25
/// 



using UnityEngine;

public class FoodDispenser : MonoBehaviour
{
    public GameObject foodClonePrefab;

    void OnMouseDown()
    {
        // Spawn clone at same position
        Instantiate(foodClonePrefab, transform.position, transform.rotation);
    }
}

