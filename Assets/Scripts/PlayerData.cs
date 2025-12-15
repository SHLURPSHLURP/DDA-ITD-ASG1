/// DDA FIREBASE PORTION
/// Script to create class
/// Made by Gracie Arianne Peh (S10265899G) 14/12/25
/// 

using System.Collections.Generic;

[System.Serializable]
public class PlayerData
{
    public int evolutionStage;
    public string stage1Mood;
    public string stage2Mood;

    public List<string> loggedMoods;
    public Dictionary<string, int> moodCount;

    public int food;
    public int toy;
    public int hunger;
    public int score;

    public List<string> evolutionsCollected;

    public PlayerData()
    {
        evolutionStage = 0;
        stage1Mood = "";
        stage2Mood = "";

        loggedMoods = new List<string>();
        moodCount = new Dictionary<string, int>()
        {
            { "happy", 0 },
            { "sad", 0 },
            { "calm", 0 }
        };

        food = 0;
        toy = 0;
        hunger = 3;
        score = 0;

        evolutionsCollected = new List<string>();
    }
}
