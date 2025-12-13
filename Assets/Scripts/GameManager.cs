using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int food = 0;
    public int toy = 0;

    public int hunger = 3;
    public int score = 0;

    public string petType = "base";
    public int evolutionStage = 0;

    public bool petPresent = false;

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
}
