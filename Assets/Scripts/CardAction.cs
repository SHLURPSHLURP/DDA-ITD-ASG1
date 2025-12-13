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
                GameManager.Instance.food++;
                HideButtonCanvas();
                break;

            case CardActionType.CollectToy:
                GameManager.Instance.toy++;
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
        if (!GameManager.Instance.petPresent)
        {
            Debug.Log("Pet not present. Scan PetCard first.");
            return;
        }

        if (string.IsNullOrEmpty(mood))
            return;

        // ✅ Log mood
        GameManager.Instance.loggedMoods.Add(mood);
        GameManager.Instance.moodCounts[mood]++;
        Debug.Log("Mood logged: " + mood);

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
    }

    void HideButtonCanvas()
    {
        if (transform.parent != null)
            transform.parent.gameObject.SetActive(false);
    }
}
