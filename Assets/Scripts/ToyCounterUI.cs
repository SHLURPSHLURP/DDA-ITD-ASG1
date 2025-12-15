/// 
/// Tiny script to change text UI to updated toys left
/// Made by Gracie Arianne Peh (S10265899G) 11/12/25
/// DUPE OF FOOD COUNTERPART WITH CHANGES FOR TOY
/// 


using UnityEngine;
using TMPro;

public class ToyCounterUI : MonoBehaviour
{
    public TMP_Text text;

    void Update() //TO CONSTANTLY UPDATE UI IF THERE IS CHANGE IN AMOUNT OF TOY ACCORDING TO GAME STATE
    {
        text.text = "Toy: " + GameState.Instance.toy;
    }
}

