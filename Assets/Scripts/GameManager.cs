/// 
/// Script to store stuff
/// Made by Gracie Arianne Peh (S10265899G) 10/12/25 
/// 




using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // -----------------------------
    // PLAYER DATA (stored in memory)
    // -----------------------------

    public int food = 0;
    public int toy = 0;

    public int hunger = 3;   // 1–3 scale (3 = full, 1 = starving)
    public int score = 0;

    public string petType = "base";  // base, happy, sad, calm, final evolutions
    public int evolutionStage = 0;   // 0=base, 1=stage1, 2=final

    public List<string> loggedMoods = new List<string>();  // stores each scan
    public Dictionary<string, int> moodCounts = new Dictionary<string, int>()
    {
        { "happy", 0 },
        { "sad", 0 },
        { "calm", 0 }
    };

    void Awake()
    {
        // Singleton Setup
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // persists across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Optional helper functions (cleaner code for CardAction & evolution)
    public void AddFood() => food++;
    public void AddToy() => toy++;
    public void LogMood(string mood)
    {
        loggedMoods.Add(mood);
        moodCounts[mood]++;
    }
}
