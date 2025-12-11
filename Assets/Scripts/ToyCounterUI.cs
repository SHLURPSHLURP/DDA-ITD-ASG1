/// 
/// Tiny script to change text UI to updated toys left
/// Made by Gracie Arianne Peh (S10265899G) 11/12/25
/// 



using UnityEngine;
using TMPro;

public class ToyCounterUI : MonoBehaviour
{
    public TMP_Text text;

    void Update()
    {
        text.text = "Toy: " + GameManager.Instance.toy;
    }
}

