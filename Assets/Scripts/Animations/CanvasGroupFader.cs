using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CanvasGroupFader : MonoBehaviour {
    
    [SerializeField]
    private CanvasGroup group;

    public float defaultFadeTime = 1f;
    public UnityEvent OnFadedIn, OnFadedOut;

    public void Fade(float alpha)
    {
        StartCoroutine(FadeCoroutine(group, alpha, defaultFadeTime));
    }

    public void Fade(float alpha, float time)
    {
        StartCoroutine(FadeCoroutine(group, alpha, time));
    }

    public void Fade(CanvasGroup group, float alpha, float time)
    {
        StartCoroutine(FadeCoroutine(group, alpha, time));
    }

    public void Fade(CanvasGroup group, float alpha)
    {
        StartCoroutine(FadeCoroutine(group, alpha, defaultFadeTime));
    }

    private IEnumerator FadeCoroutine(CanvasGroup group, float end, float time)
    {
        float start = group.alpha;
        float alpha = group.alpha;
        float timer = 0f;

        while(alpha != end)
        {
            timer += Time.unscaledDeltaTime;
            alpha = Mathf.Lerp(start, end, timer / time);
            group.alpha = alpha;

            yield return null;
        }

        if(end == 1f)
        {
            OnFadedIn.Invoke();
        }
        else if(end == 0f)
        {
            OnFadedOut.Invoke();
        }
    }

}
