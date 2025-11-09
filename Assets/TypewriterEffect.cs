using System.Collections;
using UnityEngine;
using TMPro;

public class TypewriterEffect : MonoBehaviour
{
    public TMP_Text textBox;
    [TextArea] public string fullText;
    public float delay = 0.05f;

    private TextWaveEffect waveEffect;
    private BlowOutYourCandles growEffect;

    void Awake()
    {
        textBox.text = "";
        waveEffect = GetComponent<TextWaveEffect>();
        growEffect = GetComponent<BlowOutYourCandles>();
        if (waveEffect != null)
            waveEffect.enabled = false;

    }

    public IEnumerator PlayText()
    {
        textBox.text = "";

        if (growEffect != null)
            StartCoroutine(growEffect.AnimateScale());

        foreach (char c in fullText)
        {
            textBox.text += c;
            yield return new WaitForSeconds(delay);
        }

        // Enable wave after typing finishes
        if (waveEffect != null)
            waveEffect.enabled = true;
    }
}
