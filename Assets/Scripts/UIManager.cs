using UnityEngine;
using TMPro;
using System.Collections;

public class UIManager : MonoBehaviour
{
    [Header("Main Panels")]
    public GameObject startPanel;
    public GameObject loginPanel;
    public GameObject signupPanel;
    public GameObject homePanel;

    [Header("Overlays")]
    public GameObject deathPanel;
    public TMP_Text instructionText;

    Coroutine instructionRoutine;

    void Start()
    {
        ShowStartPanel();
        HideInstruction();
        if (deathPanel != null)
            deathPanel.SetActive(false);
    }

    void Update()
    {
        // 🔴 Death check (global)
        if (deathPanel != null)
        {
            deathPanel.SetActive(GameState.Instance.petDead);
        }
    }

    // -------------------------
    // PANEL SWITCHING
    // -------------------------
    private void ShowOnly(GameObject panel)
    {
        startPanel.SetActive(false);
        loginPanel.SetActive(false);
        signupPanel.SetActive(false);
        homePanel.SetActive(false);

        panel.SetActive(true);
    }

    public void ShowStartPanel() => ShowOnly(startPanel);
    public void ShowLoginPanel() => ShowOnly(loginPanel);
    public void ShowSignupPanel() => ShowOnly(signupPanel);
    public void ShowHomePanel() => ShowOnly(homePanel);

    // -------------------------
    // INSTRUCTION TEXT
    // -------------------------
    public void ShowInstruction(string message, float duration = 2f)
    {
        if (instructionRoutine != null)
            StopCoroutine(instructionRoutine);

        instructionRoutine = StartCoroutine(InstructionRoutine(message, duration));
    }

    IEnumerator InstructionRoutine(string msg, float duration)
    {
        instructionText.text = msg;
        instructionText.gameObject.SetActive(true);
        yield return new WaitForSeconds(duration);
        HideInstruction();
    }

    void HideInstruction()
    {
        instructionText.gameObject.SetActive(false);
    }

    // -------------------------
    // DEATH PANEL BUTTON
    // -------------------------
    public void ResetPetFromDeath()
    {
        GameState.Instance.ResetPet();
        deathPanel.SetActive(false);
    }
}
