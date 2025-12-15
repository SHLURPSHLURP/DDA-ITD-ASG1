using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class GameState : MonoBehaviour
{
    public static GameState Instance;

    [Header("Resources")]
    public int food = 0;
    public int toy = 0;

    [Header("Pet Stats")]
    public int hunger = 3;          // 0 = dead, max 3
    public int score = 0;
    public bool petDead = false;

    [Header("UI (linked in Inspector)")]
    public Slider hungerSlider;
    public TMP_Text scoreText;
    public Slider evolutionProgressSlider;


    [Header("Evolution")]
    public int evolutionStage = 0;
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
     

        if (evolutionProgressSlider != null) //INITIALISE SLIDER
        {
            evolutionProgressSlider.minValue = 0;
            evolutionProgressSlider.maxValue = 10;
            evolutionProgressSlider.value = loggedMoods.Count;
        }


    }

    void Update()
    {
        UpdateUI();
    }

    // -------------------------
    // UI UPDATE
    // -------------------------
    void UpdateUI()
    {
        if (hungerSlider != null)
            hungerSlider.value = hunger;

        if (scoreText != null)
            scoreText.text = "Score: " + score;

        if (evolutionProgressSlider != null)
            evolutionProgressSlider.value = loggedMoods.Count;
    }

    // -------------------------
    // FEED PET
    // -------------------------
    public void FeedPet()
    {
        if (petDead || food <= 0) return;

        food--;
        hunger = Mathf.Clamp(hunger + 1, 0, 3);

        FirebasePlayerManager.Instance.SaveFromGameState();

    }

    // -------------------------
    // PLAY WITH PET
    // -------------------------
    public void PlayWithPet()
    {
        if (petDead || toy <= 0) return;

        toy--;
        score++;

        FirebasePlayerManager.Instance.SaveFromGameState();

    }

    // -------------------------
    // LOG MOOD (1 day passes)
    // -------------------------
    public void LogMood(string mood)
    {
        if (petDead) return;

        loggedMoods.Add(mood);
        moodCounts[mood]++;

        hunger--;
        hunger = Mathf.Clamp(hunger, 0, 3);

        if (hunger <= 0)
        {
            petDead = true;
            FirebasePlayerManager.Instance.SaveFromGameState(); //FIREBASE CRUD
            return;
        }

        CheckEvolution();
        FirebasePlayerManager.Instance.SaveFromGameState(); //FIREBASE CRUD
    }

    // -------------------------
    // EVOLUTION
    // -------------------------
    void CheckEvolution()
    {
        if (loggedMoods.Count < 10)
            return;

        if (evolutionStage == 0)
            Stage1Evolution();
        else if (evolutionStage == 1)
            FinalEvolution();
    }

    void Stage1Evolution()
    {
        stage1Mood = GetHighestMood();
        evolutionStage = 1;
        evolutionJustHappened = true;

        evolutionsCollected.Add(stage1Mood);
        ResetMoodTracking();
    }

    void FinalEvolution()
    {
        stage2Mood = GetHighestMood();
        evolutionStage = 2;
        finalEvolutionJustHappened = true;

        evolutionsCollected.Add(GetFinalMoodKey());
        ResetMoodTracking();
    }

    string GetHighestMood()
    {
        return moodCounts.OrderByDescending(x => x.Value).First().Key;
    }

    void ResetMoodTracking()
    {
        loggedMoods.Clear();
        moodCounts["happy"] = 0;
        moodCounts["sad"] = 0;
        moodCounts["calm"] = 0;

        if (evolutionProgressSlider != null)
            evolutionProgressSlider.value = 0;

    }

    public string GetFinalMoodKey()
    {
        List<string> moods = new() { stage1Mood, stage2Mood };
        moods.Sort();
        return $"{moods[0]}_{moods[1]}";
    }

    public void ResetPet()
    {
        food = 0;
        toy = 0;
        hunger = 3;
        score = 0;
        petDead = false;

        evolutionStage = 0;
        stage1Mood = "";
        stage2Mood = "";

        evolutionJustHappened = false;
        finalEvolutionJustHappened = false;

        ResetMoodTracking();
    }



//  FOR DATABASE YIPEE!!! 



        public PlayerData ToPlayerData()
    {
        PlayerData data = new PlayerData();

        data.evolutionStage = evolutionStage;
        data.stage1Mood = stage1Mood;
        data.stage2Mood = stage2Mood;

        data.loggedMoods = new List<string>(loggedMoods);
        data.moodCount = new Dictionary<string, int>(moodCounts);

        data.food = food;
        data.toy = toy;
        data.hunger = hunger;
        data.score = score;

        data.evolutionsCollected = new List<string>(evolutionsCollected);

        return data;
    }

    public void LoadFromPlayerData(PlayerData data)
    {
        evolutionStage = data.evolutionStage;
        stage1Mood = data.stage1Mood;
        stage2Mood = data.stage2Mood;

        loggedMoods = new List<string>(data.loggedMoods);
        moodCounts = new Dictionary<string, int>(data.moodCount);

        food = data.food;
        toy = data.toy;
        hunger = data.hunger;
        score = data.score;

        evolutionsCollected = new List<string>(data.evolutionsCollected);
    }

    public void ResetPetAndSave()
    {
        ResetPet();
        FirebasePlayerManager.Instance.SaveFromGameState();
    }


}
