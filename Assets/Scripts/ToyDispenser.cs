using UnityEngine;

public class ToyDispenser : MonoBehaviour
{
    public GameObject toyClonePrefab;

    [Header("Pet References (drag from PetCard)")]
    public Transform snapZone;
    public PetSpinReaction petSpin;

    public void SpawnToy()
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

        GameState.Instance.toy--;
    }
}
