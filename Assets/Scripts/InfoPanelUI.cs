/// 
/// to control info panel UI
/// Made by Gracie Arianne Peh (S10265899G) 13/12/25
/// 

using UnityEngine;
using TMPro;
using System.Text;
using Firebase.Auth;


public class InfoPanelUI : MonoBehaviour
{

    public TMP_Text userIdText;
    public TMP_Text emailText;
    public TMP_Text moodListText;
    public TMP_Text evolutionListText;

    void OnEnable()
    {
        Refresh();
    }


    // REFRESH UI
    public void Refresh()
    {

        // AUTH INFO (UID + EMAIL)
        FirebaseUser user = FirebaseAuth.DefaultInstance.CurrentUser;

        if (user != null)
        {
            userIdText.text = "UID: " + user.UserId;
            emailText.text = "Email: " + user.Email;
        }
        else
        {
            userIdText.text = "UID: -";
            emailText.text = "Email: -";
        }



        // LOGGED MOODS
        StringBuilder moodSB = new StringBuilder();
        moodSB.AppendLine("Logged Moods:");

        if (GameState.Instance.loggedMoods.Count == 0)
        {
            moodSB.AppendLine("- None");
        }
        else
        {
            foreach (string mood in GameState.Instance.loggedMoods)
            {
                moodSB.AppendLine("- " + Capitalize(mood));
            }
        }

        moodListText.text = moodSB.ToString();

        // EVOLUTION HISTORY
        StringBuilder evoSB = new StringBuilder();
        evoSB.AppendLine("Evolutions Collected:");

        if (GameState.Instance.evolutionsCollected.Count == 0)
        {
            evoSB.AppendLine("- None");
        }
        else
        {
            foreach (string evo in GameState.Instance.evolutionsCollected)
            {
                evoSB.AppendLine("- " + FormatEvolutionName(evo));
            }
        }

        evolutionListText.text = evoSB.ToString();
    }


    // BUTTON ACTIONS
    public void ResetPetButton()
    {
        GameState.Instance.ResetPetAndSave();
        Refresh();
    }

    // HELPERS
    string Capitalize(string s)
    {
        if (string.IsNullOrEmpty(s)) return s;
        return char.ToUpper(s[0]) + s.Substring(1);
    }

    string FormatEvolutionName(string key)
    {
        return key switch
        {
            // Stage 1 
            "happy" => "Bright",
            "sad" => "Somber",
            "calm" => "Serene",

            // Final evolutions
            "happy_happy" => "Joyful",
            "sad_sad" => "Sorrowful",
            "calm_calm" => "Tranquil",
            "happy_sad" => "Bittersweet",
            "happy_calm" => "Content",
            "calm_sad" => "Resigned",

            _ => key
        };
    }
}
