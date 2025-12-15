/// 
/// script to control the TOY dispenser (to spawn cloned tpy)
/// Made by Gracie Arianne Peh (S10265899G) 11/12/25
/// DUPE OF FOOD COUNTERPART WITH CHANGES FOR TOY
/// 

using UnityEngine;

public class ToyDispenser : MonoBehaviour
{
    public GameObject toyClonePrefab;

//PET REFERENCES FOR LOCATION OF WHERE FOOD SHOULD BE DETECTED AND WHAT ANIMATION PET CARRIES OUT
    public Transform snapZone;
    public PetSpinReaction petSpin;

    public void SpawnToy() //SPAWN FOOD CLONE THEN CALL FOODCONSUMEONRELEASE AND CONTROL PET REACTION
    {
        if (GameState.Instance.toy <= 0)
            return;

        GameObject toy = Instantiate(
            toyClonePrefab,
            transform.position + Vector3.up * 0.05f,
            Quaternion.identity
        );

        ToyConsumeOnRelease consume = toy.GetComponent<ToyConsumeOnRelease>();
        consume.snapZone = snapZone;
        consume.petSpin = petSpin;

       
    }
}
