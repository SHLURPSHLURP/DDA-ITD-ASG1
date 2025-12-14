using UnityEngine;
using TMPro;

public class EvolutionUIController : MonoBehaviour
{
    [Header("Stage 1 UI")]
    public GameObject stage1Panel;
    public TMP_Text stage1NameText;
    public TMP_Text stage1DescText;

    [Header("Final Evolution UI")]
    public GameObject finalPanel;
    public TMP_Text finalNameText;
    public TMP_Text finalDescText;

    void Update()
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

    void ShowStage1()
    {
        stage1Panel.SetActive(true);

        string mood = GameState.Instance.stage1Mood;
        stage1NameText.text = mood.ToUpper();

        stage1DescText.text = mood switch
        {
            "happy" => "Your pet feels bright and joyful.",
            "sad" => "Your pet has grown more sensitive.",
            "calm" => "Your pet has found inner peace.",
            _ => "Your pet has evolved."
        };
    }

    void ShowFinal()
    {
        finalPanel.SetActive(true);

        string finalName = GameState.Instance.stage1Mood + GameState.Instance.stage2Mood;
        finalNameText.text = finalName.ToUpper();

        finalDescText.text = "Your pet has reached its final form.";
    }

    // -------------------------
    // BUTTON HOOKS
    // -------------------------
    public void ContinueFromStage1()
    {
        stage1Panel.SetActive(false);
    }

    public void ResetPetFromFinal()
    {
        GameState.Instance.ResetPet();
        finalPanel.SetActive(false);
    }
}
