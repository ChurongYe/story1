using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypewriterEffect : MonoBehaviour
{
    public Text uiText;
    [TextArea]
    public string fullText;
    public float delay = 0.05f;

    private Coroutine typingCoroutine;

    void Start()
    {
        StartTyping();
    }

    public void StartTyping()
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        uiText.text = "";
        foreach (char c in fullText)
        {
            uiText.text += c;
            yield return new WaitForSeconds(delay);
        }
    }
}