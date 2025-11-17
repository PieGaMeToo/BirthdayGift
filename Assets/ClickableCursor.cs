using System.Collections;
using UnityEngine;

public class ClickableCursor : MonoBehaviour
{
    [Header("Target Candle")]
    public Transform clickTarget;

    [Header("Cursor Settings")]
    public Texture2D cursorTexture;
    public Vector2 hotspot = Vector2.zero;

    private bool isHovering = false;
    private Coroutine shrinkCoroutine;

    private void Update()
    {
        if (clickTarget == null) return;

        transform.position = clickTarget.position;
        transform.rotation = clickTarget.rotation;

        HandleHoverCheck();

        if (isHovering && Input.GetMouseButtonDown(0))
        {
            DoClickAction();
        }
    }
    private void HandleHoverCheck()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        bool hitHelper = false;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform == transform)
            {
                hitHelper = true;

                if (!isHovering)
                {
                    isHovering = true;
                    Cursor.SetCursor(cursorTexture, hotspot, CursorMode.ForceSoftware);
                }
            }
        }

        if (isHovering && !hitHelper)
        {
            isHovering = false;
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }

    private void DoClickAction()
    {
        if (shrinkCoroutine == null)
        {
            shrinkCoroutine = StartCoroutine(ShrinkToZero(clickTarget, 1f));
        }
    }

    private IEnumerator ShrinkToZero(Transform target, float duration)
    {
        if (duration <= 0f)
        {
            target.localScale = Vector3.zero;
            shrinkCoroutine = null;
            yield break;
        }

        Vector3 startScale = target.localScale;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            target.localScale = Vector3.Lerp(startScale, Vector3.zero, t);
            yield return null;
        }

        target.localScale = Vector3.zero;
        shrinkCoroutine = null;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        gameObject.SetActive(false);
    }
}
