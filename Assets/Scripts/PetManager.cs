/// 
/// Script to handle pet related functions like feeding etc
/// Made by Gracie Arianne Peh (S10265899G) 9/12/25 (prototype, no database integration yet)
/// 


using UnityEngine;
using TMPro;

public class PetManager : MonoBehaviour
{
    public int hunger = 3;   // 1–3, 0 = dead
    public int score = 0;    // increases when playing with toy

    public TMP_Text hungerText;
    public TMP_Text scoreText;

    void Start() // UI (hunger bar, score is updated when start)
    {
        UpdateUI();
    }

    public void FeedPet() //function for feeding pet, feeding will increase hunger bar
    {
        if (hunger < 3)
            hunger++;

        UpdateUI();
    }

    public void PlayWithPet() //function for playing with pet 
    {
        score++;
        UpdateUI();
    }

    public void UpdateUI() //function to change the UI text
    {
        hungerText.text = "Fed: " + hunger;
        scoreText.text = "Score: " + score;
    }
}
