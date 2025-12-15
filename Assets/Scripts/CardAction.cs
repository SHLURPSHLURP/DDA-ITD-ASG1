/// SOME DDA FIREBASE INSIDE (CALLING FIREBASEMANAGER TO UPDATE FIREBASE FOR TOY AND FOOD)
/// script for spawned prefabs on card and what they do
/// Made by Gracie Arianne Peh (S10265899G) 11/12/25
/// Some reference to ChatGPT
/// 

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

    public string mood;

    public void ExecuteAction() //TO CARRY OUT THE LISTED ACTION (EITHER LOGGIN MOOD OR COLLECTION, ALSO PLAYING SOUND AND UPDATING DATABASE)
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
        PetDialogue dialogue = FindObjectOfType<PetDialogue>(); //CALLING PETDIALOGUE OBJECT TO BE ABLE TO CHANGE IT

        // IF PET NOT PRESENT
        if (!GameState.Instance.petPresent)
        {
            UIManager ui = FindObjectOfType<UIManager>();
            if (ui != null)
            {
                ui.ShowInstruction("Scan your pet card first!");
            }
            return;

        }

        // LOCK MOOD LOGGING AFTER FINAL EVOLUTION STAGE REACHED AND RELEVANT SCRIPTS TO INDICATE SO
        if (GameState.Instance.evolutionStage >= 2)
        {
            if (dialogue != null)
                dialogue.SayFinalLocked(); 
            return;
        }

        if (string.IsNullOrEmpty(mood))
            return;

        //UPDATE MOODS LOGGED AND PLAY SOUND (REFERENCING GAME STATE AND AUDIO MANAGER)
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
