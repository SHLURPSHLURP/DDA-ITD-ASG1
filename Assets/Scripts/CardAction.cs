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
                AudioManager.Instance.PlayDing();
                FirebasePlayerManager.Instance.SaveFromGameState(); 
                break;

            case CardActionType.CollectToy:
                GameState.Instance.toy++;
                AudioManager.Instance.PlayDing();
                FirebasePlayerManager.Instance.SaveFromGameState();
                break;

  
        }
    }

    void HandleLogMood()
    {
        PetDialogue dialogue = FindObjectOfType<PetDialogue>();

        // ❌ Pet not present
        if (!GameState.Instance.petPresent)
        {
            UIManager ui = FindObjectOfType<UIManager>();
            if (ui != null)
            {
                ui.ShowInstruction("Scan your pet card first!");
            }
            return;

        }

        // ❌ Final evolution reached → lock mood logging
        if (GameState.Instance.evolutionStage >= 2)
        {
            if (dialogue != null)
                dialogue.SayFinalLocked(); // "I've grown enough for now"
            return;
        }

        if (string.IsNullOrEmpty(mood))
            return;

        // ✅ Log mood (this also checks evolution internally)
        GameState.Instance.LogMood(mood);
        AudioManager.Instance.PlayDing();


        // Pet dialogue reaction
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
}
