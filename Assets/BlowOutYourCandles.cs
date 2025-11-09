using System.Collections;
using UnityEngine;

public class BlowOutYourCandles : MonoBehaviour
{
    private float scaleX = 0f;
    public float growSpeed = 1.5f;

    private void Start()
    {
        transform.localScale = new Vector3(0f, 1f, 1f);
    }

    public IEnumerator AnimateScale()
    {
        yield return StartCoroutine(ScaleTo(0.33f));

        yield return StartCoroutine(ScaleTo(-0.66f));

        yield return StartCoroutine(ScaleTo(1f));
    }

    private IEnumerator ScaleTo(float targetScaleX)
    {
        float start = scaleX;
        float elapsed = 0f;
        float duration = Mathf.Abs(targetScaleX - start) / growSpeed;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            scaleX = Mathf.Lerp(start, targetScaleX, elapsed / duration);
            transform.localScale = new Vector3(scaleX, 1f, 1f);
            yield return null;
        }

        scaleX = targetScaleX;
        transform.localScale = new Vector3(scaleX, 1f, 1f);
    }
}
