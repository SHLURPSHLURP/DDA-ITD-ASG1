/// 
/// Tiny script to spawn draggable clone from "dispenser" toy (inspired by pikmin bloom!!!)
/// Made by Gracie Arianne Peh (S10265899G) 11/12/25
/// 



using UnityEngine;

public class ToyDispenser : MonoBehaviour
{
    public GameObject toyClonePrefab;

    void OnMouseDown()
    {
        // Spawn clone at same position
        Instantiate(toyClonePrefab, transform.position, transform.rotation);
    }
}

