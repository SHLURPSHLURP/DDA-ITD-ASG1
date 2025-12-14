using UnityEngine;
using System.Collections.Generic;

public class GameState : MonoBehaviour
{
    public static GameState Instance;

    [Header("Resources")]
    public int food = 0;
    public int toy = 0;

    [Header("Pet Stats")]
    public int hunger = 3;          // TEMPORARILY UNUSED
    public int score = 0;
    public bool petDead = false;    // TEMPORARILY UNUSED

    [Header("Evolution")]
    public int evolutionStage = 0;   // 0 = base, 1 = stage1, 2 = final
    public string stage1Mood = "";
    public string stage2Mood = "";

    public bool evolutionJustHappened = false;
    public bool finalEvolutionJustHappened = false;

    public List<string> evolutionsCollected = new();

    [Header("Mood Tracking")]
    public List<string> loggedMoods = new();
    public Dictionary<string, int> moodCounts = new()
    {
        { "happy", 0 },
        { "sad", 0 },
        { "calm", 0 }
    };

    [Header("AR State")]
    public bool petPresent = false;

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
    // LOG MOOD (HUNGER DISABLED)
    // -------------------------
    public void LogMood(string mood)
    {
        // if (petDead)
        //     return;

        loggedMoods.Add(mood);
        moodCounts[mood]++;

        // --- Hunger logic DISABLED for testing ---
        /*
        hunger--;
        hunger = Mathf.Clamp(hunger, 0, 3);

        if (hunger == 0)
        {
            petDead = true;
            return;
        }
        */

        CheckEvolution();
    }

    // -------------------------
    // EVOLUTION CHECK
    // -------------------------
    void CheckEvolution()
    {
        if (loggedMoods.Count < 10)
            return;

        if (evolutionStage == 0)
        {
            Stage1Evolution();
        }
        else if (evolutionStage == 1)
        {
            FinalEvolution();
        }
    }

    void Stage1Evolution()
    {
        stage1Mood = GetHighestMood();
        evolutionStage = 1;
        evolutionJustHappened = true;

        evolutionsCollected.Add(stage1Mood);
        ResetMoodTracking();

        Debug.Log("Stage 1 Evolution: " + stage1Mood);
    }

    void FinalEvolution()
    {
        stage2Mood = GetHighestMood();
        evolutionStage = 2;
        finalEvolutionJustHappened = true;

        string finalName = stage1Mood + stage2Mood;
        evolutionsCollected.Add(finalName);
        ResetMoodTracking();

        Debug.Log("Final Evolution: " + finalName);
    }

    // -------------------------
    // HELPERS
    // -------------------------
    string GetHighestMood()
    {
        string highest = "";
        int max = -1;

        foreach (var pair in moodCounts)
        {
            if (pair.Value > max)
            {
                max = pair.Value;
                highest = pair.Key;
            }
        }

        return highest;
    }

    void ResetMoodTracking()
    {
        loggedMoods.Clear();
        moodCounts["happy"] = 0;
        moodCounts["sad"] = 0;
        moodCounts["calm"] = 0;
    }

    // -------------------------
    // RESET PET (FOR FINAL EVOLUTION)
    // -------------------------
    public void ResetPet()
    {
        food = 0;
        toy = 0;

        // hunger = 3;
        // petDead = false;

        evolutionStage = 0;
        stage1Mood = "";
        stage2Mood = "";

        evolutionJustHappened = false;
        finalEvolutionJustHappened = false;

        ResetMoodTracking();
    }
}
