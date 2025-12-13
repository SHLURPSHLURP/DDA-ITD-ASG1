using UnityEngine;

public class CardAction : MonoBehaviour
{
    public enum CardActionType
    {
        CollectFood,
        CollectToy,
        LogMood,
        PetInteraction
    }

    public CardActionType actionType;

    [Header("Mood Only")]
    public string mood;
    public string imageName;

    public void ExecuteAction()
    {
        switch (actionType)
        {
            case CardActionType.LogMood:
                HandleLogMood();
                break;

            case CardActionType.CollectFood:
                GameState.Instance.food++;
                HideButtonCanvas();
                break;

            case CardActionType.CollectToy:
                GameState.Instance.toy++;
                HideButtonCanvas();
                break;

            case CardActionType.PetInteraction:
                UnityEngine.SceneManagement.SceneManager.LoadScene("FeedPlayScene");
                HideButtonCanvas();
                break;
        }
    }

    void HandleLogMood()
    {
        // ❌ Require pet
        if (!GameState.Instance.petPresent)
        {
            Debug.Log("Pet not present. Scan PetCard first.");
            return;
        }

        if (string.IsNullOrEmpty(mood))
            return;

        // ✅ Log mood
        GameState.Instance.loggedMoods.Add(mood);
        GameState.Instance.moodCounts[mood]++;
        Debug.Log("Mood logged: " + mood);

        // Trigger pet dialogue override
        PetDialogue dialogue = FindObjectOfType<PetDialogue>();
        if (dialogue != null)
        {
            dialogue.SayMoodLogged();
        }


        // 🔍 Scale pulse reaction
        PetScalePulse pulse = FindObjectOfType<PetScalePulse>();
        if (pulse != null)
        {
            pulse.PlayPulse();
        }
        else
        {
            Debug.Log("PetScalePulse not found on PetCard.");
        }

        // ✅ Remove mood card safely
        CardTrackingManager tracker = FindObjectOfType<CardTrackingManager>();
        if (tracker != null)
        {
            tracker.MarkImageAsConsumed(imageName);
        }

        InfoPanelUI info = FindObjectOfType<InfoPanelUI>();
        if (info != null)
            info.Refresh();

    }

    void HideButtonCanvas()
    {
        if (transform.parent != null)
            transform.parent.gameObject.SetActive(false);
    }
}