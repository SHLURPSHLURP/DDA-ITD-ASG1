/// 
/// script to check for then automatically change to relevant panels during evolution
/// Made by Gracie Arianne Peh (S10265899G) 12/12/25
/// 

using UnityEngine;
using TMPro;

public class EvolutionUIController : MonoBehaviour
{
    //LINK TO STAGE 1 EVO UI
    public GameObject stage1Panel;
    public TMP_Text stage1NameText;
    public TMP_Text stage1DescText;

    //LINK TO FINAL EVO UI
    public GameObject finalPanel;
    public TMP_Text finalNameText;
    public TMP_Text finalDescText;

    void Update() //TO CONSTANTLY CHECK IF UPDATE HAPPENED AND WHAT TO DO
    {
        if (GameState.Instance.evolutionJustHappened)
        {
            ShowStage1();
            GameState.Instance.evolutionJustHappened = false;
        }

        if (GameState.Instance.finalEvolutionJustHappened)
        {
            ShowFinal();
            GameState.Instance.finalEvolutionJustHappened = false;
        }
    }


    void ShowStage1() //FIRST STAGE EVOLUTION PAGE --> SHOWS THE NAME AND CORRESSPONDING DESC
    {
        stage1Panel.SetActive(true);

        string mood = GameState.Instance.stage1Mood;
        stage1NameText.text = GetStage1Name(mood);

        stage1DescText.text = mood switch
        {
            "happy" => "Your pet feels bright and joyful.",
            "sad" => "Your pet has grown more sensitive.",
            "calm" => "Your pet is calmer.",
            _ => "Your pet has evolved."
        };
    }

 
    // FINAL EVOLUTION PAGE--> SHOWS NAME AND DESC
    void ShowFinal()
    {
        finalPanel.SetActive(true);

        string key = GameState.Instance.GetFinalMoodKey();
        finalNameText.text = GetFinalName(key);

        finalDescText.text = "Your pet has reached its final evolution, thank you for taking care of MoodyPet :) ";
    }

  
    // TO CONTINUE: OPEN HOME PANEL 
    public void ContinueFromStage1()
    {
        stage1Panel.SetActive(false);
    }

    // SEE PET WITHOUT RESETTING IT FIRST
    public void ConfirmFinalEvolution()
    {
        finalPanel.SetActive(false);
        Debug.Log("Final evolution confirmed. Pet can now be viewed.");
    }

    //RESET PET--> CLEAR LOGGED MOODS AND STAGE EVOLUTION IF THERE IS
    public void ResetPetFromFinal() 
    {
        GameState.Instance.ResetPet();
        finalPanel.SetActive(false);
    }


    // NAME HELPERS--> GET PROPER EVOLUTION NAME FROM MOODS LOGGED

    string GetStage1Name(string mood)
    {
        return mood switch
        {
            "happy" => "Bright",
            "sad" => "Somber",
            "calm" => "Serene",
            _ => mood.ToUpper()
        };
    }

    string GetFinalName(string key)
    {
        return key switch
        {
            "happy_happy" => "Joyful",
            "sad_sad" => "Sorrowful",
            "calm_calm" => "Tranquil",
            "happy_sad" => "Bittersweet",
            "happy_calm" => "Content",
            "calm_sad" => "Resigned",
            _ => key.ToUpper()
        };
    }
}
