/// 
/// Tiny script to change text UI to updated food left
/// Made by Gracie Arianne Peh (S10265899G) 11/12/25
/// 



using UnityEngine;
using TMPro;

public class FoodCounterUI : MonoBehaviour
{
    public TMP_Text text;

    void Update()
    {
        text.text = "Food: " + GameManager.Instance.food;
    }
}
