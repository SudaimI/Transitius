using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    [TextArea]
    public string text;

    public float delayBeforeNext = 5f;
}

public class TypewriterEffect : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public float letterDelay = 0.05f;

    public List<DialogueLine> dialogueLines = new List<DialogueLine>();

    private void Start()
    {
        StartCoroutine(PlayDialogue());
    }

    IEnumerator PlayDialogue()
    {
        foreach (DialogueLine line in dialogueLines)
        {
            dialogueText.text = "";

            foreach (char letter in line.text)
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(letterDelay);
            }

            yield return new WaitForSeconds(line.delayBeforeNext);
        }

        dialogueText.text = "";
    }
}