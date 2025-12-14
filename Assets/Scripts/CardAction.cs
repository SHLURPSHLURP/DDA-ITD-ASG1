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
        if (!GameState.Instance.petPresent)
        {
            Debug.Log("Pet not present. Scan PetCard first.");
            return;
        }

        if (string.IsNullOrEmpty(mood))
            return;

        // ✅ Log mood (this also checks evolution)
        GameState.Instance.LogMood(mood);

        // Pet dialogue
        PetDialogue dialogue = FindObjectOfType<PetDialogue>();
        if (dialogue != null)
            dialogue.SayMoodLogged();

        // Visual reaction
        PetScalePulse pulse = FindObjectOfType<PetScalePulse>();
        if (pulse != null)
            pulse.PlayPulse();

        // Info page update
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
