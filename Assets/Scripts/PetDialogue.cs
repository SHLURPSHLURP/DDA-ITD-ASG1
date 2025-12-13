using UnityEngine;
using TMPro;
using System.Collections;

public class PetDialogue : MonoBehaviour
{
    public TMP_Text textBox;

    [Header("Idle Dialogue")]
    public string[] idleLines;
    public float idleInterval = 3f;

    [Header("Mood Response")]
    public string moodLoggedLine = "I'll remember that :)";

    private Coroutine idleCoroutine;

    void Start()
    {
        StartIdleDialogue();
    }

    // ------------------------
    // IDLE DIALOGUE
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
    // TEMPORARY OVERRIDE
    // ------------------------
    public void SayMoodLogged()
    {
        if (idleCoroutine != null)
            StopCoroutine(idleCoroutine);

        StartCoroutine(MoodLoggedRoutine());
    }

    IEnumerator MoodLoggedRoutine()
    {
        textBox.text = moodLoggedLine;
        yield return new WaitForSeconds(2f);
        StartIdleDialogue();
    }
}