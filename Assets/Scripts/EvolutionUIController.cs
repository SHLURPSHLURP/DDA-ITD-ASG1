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

    // -------------------------
    // STAGE 1
    // -------------------------
    void ShowStage1()
    {
        stage1Panel.SetActive(true);

        string mood = GameState.Instance.stage1Mood;
        stage1NameText.text = GetStage1Name(mood);

        stage1DescText.text = mood switch
        {
            "happy" => "Your pet feels bright and joyful.",
            "sad" => "Your pet has grown more sensitive.",
            "calm" => "Your pet has found inner peace.",
            _ => "Your pet has evolved."
        };
    }

    // -------------------------
    // FINAL EVOLUTION
    // -------------------------
    void ShowFinal()
    {
        finalPanel.SetActive(true);

        string key = GameState.Instance.GetFinalMoodKey();
        finalNameText.text = GetFinalName(key);

        finalDescText.text = "Your pet has reached its final emotional form.";
    }

    // -------------------------
    // BUTTON HOOKS
    // -------------------------
    public void ContinueFromStage1()
    {
        stage1Panel.SetActive(false);
    }

    // 🔑 NEW: SEE PET BUTTON
    public void ConfirmFinalEvolution()
    {
        finalPanel.SetActive(false);

        // Do NOT reset pet
        // Pet stays in final form
        // Mood logging already stops naturally after final

        Debug.Log("Final evolution confirmed. Pet can now be viewed.");
    }

    public void ResetPetFromFinal()
    {
        GameState.Instance.ResetPet();
        finalPanel.SetActive(false);
    }

    // -------------------------
    // NAME HELPERS
    // -------------------------
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
