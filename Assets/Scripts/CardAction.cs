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
        // ❌ Must have pet
        if (!GameState.Instance.petPresent)
        {
            Debug.Log("Pet not present.");
            return;
        }

        // ❌ Already logged today
        if (GameState.Instance.moodLoggedToday)
        {
            Debug.Log("Mood already logged today.");
            return;
        }

        if (string.IsNullOrEmpty(mood))
            return;

        // ✅ Log mood
        GameState.Instance.LogMood(mood);
        Debug.Log("Mood logged: " + mood);

        // ✅ Pet reaction
        PetScalePulse pulse = FindObjectOfType<PetScalePulse>();
        if (pulse != null)
            pulse.PlayPulse();

        PetDialogue dialogue = FindObjectOfType<PetDialogue>();
        if (dialogue != null)
            dialogue.SayMoodLogged();

        // ✅ Remove this mood card
        CardTrackingManager tracker = FindObjectOfType<CardTrackingManager>();
        if (tracker != null)
            tracker.MarkImageAsConsumed(imageName);
    }

    void HideButtonCanvas()
    {
        if (transform.parent != null)
            transform.parent.gameObject.SetActive(false);
    }
}
