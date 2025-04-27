using System.Collections;
using UnityEngine;

public static class Fader
{
    /// <summary>
    /// Tweens the CanvasGroup.alpha from start to end over duration seconds.
    /// </summary>
    public static IEnumerator Fade(float start, float end, float duration, CanvasGroup targetCanvasGroup)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime; // use unscaled so UI fade isn’t tied to timeScale
            float t = Mathf.Clamp01(elapsed / duration);
            targetCanvasGroup.alpha = Mathf.Lerp(start, end, t);
            yield return null;
        }
        targetCanvasGroup.alpha = end;
    }
}
