using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextManager : MonoBehaviour
{
    public List<TypewriterEffect> textBoxes;
    public float delayBetweenBoxes = 2.0f;

    void Start()
    {
        // Make sure all are hidden initially
        foreach (var t in textBoxes)
        {
            t.gameObject.SetActive(false);
        }

        StartCoroutine(DisplayTextBoxes());
    }

    IEnumerator DisplayTextBoxes()
    {
        foreach (var t in textBoxes)
        {
            t.gameObject.SetActive(true);
            yield return StartCoroutine(t.PlayText());
            yield return new WaitForSeconds(delayBetweenBoxes);
        }
    }
}
