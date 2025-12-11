/// 
/// Script to handle collection of toy and food and logging of mood
/// Made by Gracie Arianne Peh (S10265899G) 10/12/25 (no database yet)
/// Heavily referenced chatGPT 
/// 


using UnityEngine;

public class CardAction : MonoBehaviour
{
    public enum CardActionType //chatgpt generated i did not know how to do this 
    {
        CollectFood,
        CollectToy,
        LogMood,
        PetInteraction
    }


    public CardActionType actionType;  // This should show as a dropdown
    public string mood;                // Only used for LogMood

    public void ExecuteAction()
    {
        switch (actionType)
        {
            case CardActionType.CollectFood:
                GameManager.Instance.food++;
                Debug.Log("Food collected! Food: " + GameManager.Instance.food);
                break;

            case CardActionType.CollectToy:
                GameManager.Instance.toy++;
                Debug.Log("Toy collected! Toys: " + GameManager.Instance.toy);
                break;

            case CardActionType.LogMood:
                if (!string.IsNullOrEmpty(mood))
                {
                    GameManager.Instance.loggedMoods.Add(mood);
                    GameManager.Instance.moodCounts[mood]++;
                    Debug.Log("Mood logged: " + mood);
                }
                break;

            case CardActionType.PetInteraction:
                // Load your feed/play scene here
                UnityEngine.SceneManagement.SceneManager.LoadScene("FeedPlayScene");
                break;
        }

        // Hide this button's parent (usually the world-space canvas)
        if (transform.parent != null)
        {
            transform.parent.gameObject.SetActive(false);
        }
    }
}
