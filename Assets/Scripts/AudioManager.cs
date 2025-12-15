/// 
/// Small script to handle audio (for connecting to different game objects)
/// Made by Gracie Arianne Peh (S10265899G) 14/12/25
/// Minimal reference to ChatGPT
/// 

using UnityEngine;
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance; // so other scripts can access (referenced from chatGPT)
    public AudioSource audioSource;
    public AudioClip pageTurn;
    public AudioClip ding;
    public AudioClip pickupPop;
    public AudioClip putdownPop;

    void Awake() // so other scripts can access (referenced from chatGPT)
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

   
    // FUNCTIONS TO CALL EACH AUDIO (TO BE LINKED IN INSPECTOR IN VARIOUS OTHER OBJECTS LATER)
   
    public void PlayPageTurn()
    {
        audioSource.PlayOneShot(pageTurn);
    }

    public void PlayDing()
    {
        audioSource.PlayOneShot(ding);
    }

    public void PlayPickup()
    {
        audioSource.PlayOneShot(pickupPop);
    }

    public void PlayPutdown()
    {
        audioSource.PlayOneShot(putdownPop);
    }
}
