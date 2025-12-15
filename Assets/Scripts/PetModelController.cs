/// 
/// Script to handle pet prefabs changing according to evolution
/// Made by Gracie Arianne Peh (S10265899G) 14/12/25
/// chatgpt reference
/// 

using UnityEngine;

public class PetModelController : MonoBehaviour
{

    public GameObject baseModel;


    public GameObject brightModel;   // happy
    public GameObject somberModel;   // sad
    public GameObject sereneModel;   // calm


    public GameObject joyfulModel;        // happy_happy
    public GameObject sorrowfulModel;     // sad_sad
    public GameObject tranquilModel;      // calm_calm
    public GameObject bittersweetModel;   // happy_sad
    public GameObject contentModel;       // happy_calm
    public GameObject resignedModel;      // calm_sad

    int lastStage = -1;

    void Start() //APPLY CORRECT MODEL ACCORDING TO LOADED DATA
    {
        ApplyModel();
    }

    void Update() //CHECK IF EVO STAGE IS CHANGE
    {
        if (GameState.Instance.evolutionStage != lastStage)
            ApplyModel();
    }

    void ApplyModel() //CHANGE MODEL ACCORDING TO STAGE
    {
        lastStage = GameState.Instance.evolutionStage;
        DisableAll();

        if (lastStage == 0)
        {
            baseModel.SetActive(true);
            return;
        }

        if (lastStage == 1)
        {
            switch (GameState.Instance.stage1Mood)
            {
                case "happy": brightModel.SetActive(true); break;
                case "sad": somberModel.SetActive(true); break;
                case "calm": sereneModel.SetActive(true); break;
            }
            return;
        }

        if (lastStage == 2)
        {
            string key = GameState.Instance.GetFinalMoodKey();

            switch (key)
            {
                case "happy_happy": joyfulModel.SetActive(true); break;
                case "sad_sad": sorrowfulModel.SetActive(true); break;
                case "calm_calm": tranquilModel.SetActive(true); break;
                case "happy_sad": bittersweetModel.SetActive(true); break;
                case "happy_calm": contentModel.SetActive(true); break;
                case "calm_sad": resignedModel.SetActive(true); break;
                default: baseModel.SetActive(true); break;
            }
        }
    }

    void DisableAll() //DISABLE OTHER MODELS
    {
        baseModel.SetActive(false);

        brightModel.SetActive(false);
        somberModel.SetActive(false);
        sereneModel.SetActive(false);

        joyfulModel.SetActive(false);
        sorrowfulModel.SetActive(false);
        tranquilModel.SetActive(false);
        bittersweetModel.SetActive(false);
        contentModel.SetActive(false);
        resignedModel.SetActive(false);
    }
}
