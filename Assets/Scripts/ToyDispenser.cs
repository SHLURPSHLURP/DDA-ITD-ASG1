/// 
/// Tiny script to spawn draggable clone from "dispenser" toy (inspired by pikmin bloom!!!)
/// Made by Gracie Arianne Peh (S10265899G) 11/12/25
/// 


using UnityEngine;

public class ToyDispenser : MonoBehaviour
{
    public GameObject toyClonePrefab;

    public void SpawnToy()
    {
        if (GameManager.Instance.toy <= 0) return;

        Instantiate(
            toyClonePrefab,
            transform.position + Vector3.up * 0.05f,
            Quaternion.identity
        );
    }
}
