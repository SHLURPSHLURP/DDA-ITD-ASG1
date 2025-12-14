using UnityEngine;
using TMPro;
using System.Collections;

public class PetDialogue : MonoBehaviour
{
    public TMP_Text textBox;

    [Header("Idle Dialogue")]
    public string[] idleLines;
    public float idleInterval = 3f;

    [Header("Overrides")]
    public string moodLoggedLine = "I'll remember that :)";
    public string finalLockedLine = "I think I've grown enough for now.";

    private Coroutine idleCoroutine;

    void Start()
    {
        StartIdleDialogue();
    }

    // ------------------------
    // IDLE
    // ------------------------
    void StartIdleDialogue()
    {
        if (idleCoroutine != null)
            StopCoroutine(idleCoroutine);

        idleCoroutine = StartCoroutine(IdleDialogueRoutine());
    }

    IEnumerator IdleDialogueRoutine()
    {
        int i = 0;

        while (true)
        {
            textBox.text = idleLines[i];
            i = (i + 1) % idleLines.Length;
            yield return new WaitForSeconds(idleInterval);
        }
    }

    // ------------------------
    // TEMP OVERRIDES
    // ------------------------
    public void SayMoodLogged()
    {
        OverrideLine(moodLoggedLine);
    }

    public void SayFinalLocked()
    {
        OverrideLine(finalLockedLine);
    }

    void OverrideLine(string line)
    {
        if (idleCoroutine != null)
            StopCoroutine(idleCoroutine);

        StartCoroutine(OverrideRoutine(line));
    }

    IEnumerator OverrideRoutine(string line)
    {
        textBox.text = line;
        yield return new WaitForSeconds(2f);
        StartIdleDialogue();
    }
}
