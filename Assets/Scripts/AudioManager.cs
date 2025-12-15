using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Source")]
    public AudioSource audioSource;

    [Header("Clips")]
    public AudioClip pageTurn;
    public AudioClip ding;
    public AudioClip pickupPop;
    public AudioClip putdownPop;

    void Awake()
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

    // -------------------------
    // SIMPLE PLAY METHODS
    // -------------------------
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
