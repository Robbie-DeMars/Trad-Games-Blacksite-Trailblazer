using JetBrains.Annotations;
using NUnit.Framework.Internal.Filters;
using System.Collections;
using TMPro;
using UnityEngine;

public class WaterfallText : MonoBehaviour
{
    string text = "";
    [SerializeField] private TMP_Text displayText;
    public float delay = 0.1f;
    private Coroutine waterfalling = null;

    private void Awake()
    {
        SetText("");
    }

    /// <summary>
    /// Initiates changing the text over time to the given parameter.
    /// </summary>
    /// <param name="Text"></param>
    public void SetText(string Text)
    {
        if (waterfalling != null)
            StopCoroutine(waterfalling);
        text = Text;
        waterfalling = StartCoroutine(RevealText());
    }

    /// <summary>
    /// Slowly reveals text character by character with the given delay, ended early by the press of a space bar.
    /// </summary>
    /// <returns></returns>
    private IEnumerator RevealText()
    {
        int currentDisplayed = 0;
        while (currentDisplayed < text.Length)
        {
            if (Input.GetKey(KeyCode.Space))
                currentDisplayed = text.Length;

            displayText.text = text.Substring(0, currentDisplayed);
            currentDisplayed++;

            yield return new WaitForSeconds(delay);
        }
        displayText.text = text;
    }
}
