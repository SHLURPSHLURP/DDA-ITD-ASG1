using UnityEngine;
using Firebase.Database;
using Firebase.Auth;
using System.Threading.Tasks;

public class FirebasePlayerManager : MonoBehaviour
{
    public static FirebasePlayerManager Instance;

    private DatabaseReference dbRef;
    private FirebaseAuth auth;
    private string userId;

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
            return;
        }

        auth = FirebaseAuth.DefaultInstance;
        dbRef = FirebaseDatabase.DefaultInstance.RootReference;
    }

    // =========================
    // LOGIN → LOAD EXISTING
    // =========================
    public async void LoadExistingPlayer()
    {
        if (auth.CurrentUser == null)
        {
            Debug.LogWarning("LoadExistingPlayer called without login.");
            return;
        }

        userId = auth.CurrentUser.UserId;

        DataSnapshot snapshot =
            await dbRef.Child("players").Child(userId).GetValueAsync();

        if (snapshot.Exists)
        {
            Debug.Log("Existing player loaded.");

            PlayerData data =
                JsonUtility.FromJson<PlayerData>(snapshot.GetRawJsonValue());

            GameState.Instance.LoadFromPlayerData(data);
        }
        else
        {
            Debug.Log("No data found. Creating new player.");
            InitializeNewPlayer();
        }
    }

    // =========================
    // SIGN UP → CREATE NEW
    // =========================
    public void InitializeNewPlayer()
    {
        if (auth.CurrentUser == null)
            return;

        userId = auth.CurrentUser.UserId;

        PlayerData newData = new PlayerData();
        SavePlayer(newData);
        GameState.Instance.LoadFromPlayerData(newData);

        Debug.Log("New player initialized.");
    }

    // =========================
    // SAVE
    // =========================
    public void SaveFromGameState()
    {
        if (auth.CurrentUser == null) return;

        PlayerData data = GameState.Instance.ToPlayerData();
        SavePlayer(data);
    }

    void SavePlayer(PlayerData data)
    {
        string json = JsonUtility.ToJson(data);
        dbRef.Child("players").Child(userId).SetRawJsonValueAsync(json);
    }

    // =========================
    // DELETE ACCOUNT
    // =========================
    public async void DeleteAccountAndData()
    {
        if (auth.CurrentUser == null)
            return;

        string uid = auth.CurrentUser.UserId;

        await dbRef.Child("players").Child(uid).RemoveValueAsync();
        await auth.CurrentUser.DeleteAsync();

        GameState.Instance.ResetPet();

        Debug.Log("Account and data deleted.");

        FindObjectOfType<UIManager>().ShowStartPanel();
    }
}
