/// 
/// Script to handle simple coroutine that cycles through text lines specified in inspector
/// Made by Gracie Arianne Peh (S10265899G) 10/12/25 (no database yet)
/// 

using UnityEngine;
using TMPro;
using System.Collections;

public class PetDialogue : MonoBehaviour
{
    public TMP_Text textBox;
    public string[] lines;

    void Start()
    {
        StartCoroutine(CycleLines());
    }

    IEnumerator CycleLines()
    {
        int i = 0;
        while (true)
        {
            textBox.text = lines[i];
            i = (i + 1) % lines.Length;
            yield return new WaitForSeconds(3f);
        }
    }
}
