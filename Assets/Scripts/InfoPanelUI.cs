using UnityEngine;
using TMPro;
using System.Text;

public class InfoPanelUI : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text usernameText;
    public TMP_Text moodListText;

    void OnEnable()
    {
        Refresh();
    }

    // Call this whenever data changes
    public void Refresh()
    {
        // Placeholder username
        usernameText.text = "Username: Guest";

        // Build mood list
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Logged Moods:");

        foreach (string mood in GameState.Instance.loggedMoods)
        {
            sb.AppendLine("- " + Capitalize(mood));
        }

        moodListText.text = sb.ToString();
    }

    string Capitalize(string s)
    {
        if (string.IsNullOrEmpty(s)) return s;
        return char.ToUpper(s[0]) + s.Substring(1);
    }
}
