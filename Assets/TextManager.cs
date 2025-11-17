using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextManager : MonoBehaviour
{
    public List<TypewriterEffect> textBoxes;
    public float delayBetweenBoxes = 2.0f;
    public GameObject cake;

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
        // Finished displaying all text boxes

        yield return new WaitForSeconds(2.0f);
        StartCoroutine(FadeToZeroAlphaImage());
        foreach (var t in textBoxes)
        {
            if(t.GetComponent<TextWaveEffect>() != null)
            {
                t.GetComponent<TextWaveEffect>().enabled = false;
            }
            StartCoroutine(FadeToZeroAlphaText(t.GetComponent<TMP_Text>()));
        }

        yield return new WaitForSeconds(2.0f);
        cake.SetActive(true);
        gameObject.SetActive(false);
    }

    private IEnumerator FadeToZeroAlphaImage()
    {
        Image backgroundImage = GetComponent<Image>();
        Color originalColor = backgroundImage.color;
        float duration = 2.0f;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / duration);
            backgroundImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }
        backgroundImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
    }
    private IEnumerator FadeToZeroAlphaText(TMP_Text textComponent)
    {
        Color originalColor = textComponent.color;
        float duration = 2.0f;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / duration);
            textComponent.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }
        textComponent.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
    }
}
