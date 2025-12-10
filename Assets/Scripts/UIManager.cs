using UnityEngine;
/// 
/// Script to handle switching panels (pages of UI) by enabling/ disabling them
/// Made by Gracie Arianne Peh (S10265899G) 9/12/25
/// 
public class UIManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject startPanel;
    public GameObject loginPanel;
    public GameObject signupPanel;
    public GameObject homePanel;
    public GameObject infoPanel;


    void Start()
    {
        ShowStartPanel();   // Begin on Start panel
    }

    // disable all first
    private void ShowOnly(GameObject panel)
    {
        startPanel.SetActive(false);
        loginPanel.SetActive(false);
        signupPanel.SetActive(false);
        homePanel.SetActive(false);
        infoPanel.SetActive(false);

        panel.SetActive(true);
    }

// different functions to call in unity ("on click") when switching between panels 
    public void ShowStartPanel()
    {
        ShowOnly(startPanel);
    }

    public void ShowLoginPanel()
    {
        ShowOnly(loginPanel);
    }

    public void ShowSignupPanel()
    {
        ShowOnly(signupPanel);
    }

    public void ShowHomePanel()
    {
        ShowOnly(homePanel);
    }

    public void ShowInfoPanel()
    {
        ShowOnly(infoPanel);
    }
}
