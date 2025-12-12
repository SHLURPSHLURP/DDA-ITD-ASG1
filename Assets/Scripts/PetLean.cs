/// 
/// Script to handle pet leaning towards food when feeding
/// Made by Gracie Arianne Peh (S10265899G) 12/12/25
/// chatgpt reference
/// 

using UnityEngine;

public class PetLean : MonoBehaviour
{
    public float leanAmount = 10f;
    public float leanSpeed = 5f;

    void Update()
    {
        bool foodExists = GameObject.FindWithTag("FoodClone") != null;

        Quaternion targetRotation = foodExists
            ? Quaternion.Euler(-leanAmount, 0, 0)
            : Quaternion.identity;

        transform.localRotation = Quaternion.Lerp(
            transform.localRotation,
            targetRotation,
            Time.deltaTime * leanSpeed
        );
    }
}
