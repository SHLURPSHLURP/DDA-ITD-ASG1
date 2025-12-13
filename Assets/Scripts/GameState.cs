using UnityEngine;
using System.Collections.Generic;

public class GameState : MonoBehaviour
{
    public static GameState Instance;

    public int food = 0;
    public int toy = 0;

    public int hunger = 3;
    public int score = 0;

    public string petType = "base";
    public int evolutionStage = 0;

    public bool petPresent = false;

    // 🔑 NEW
    public bool moodLoggedToday = false;

    public List<string> loggedMoods = new();
    public Dictionary<string, int> moodCounts = new()
    {
        { "happy", 0 },
        { "sad", 0 },
        { "calm", 0 }
    };

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

    public void LogMood(string mood)
    {
        loggedMoods.Add(mood);
        moodCounts[mood]++;
        moodLoggedToday = true;
    }
}
