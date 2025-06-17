using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : Singleton<FadeManager>
{
    [SerializeField] private Image fadeImage;
    [SerializeField] private float defaultFadeDuration = 1f;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        FadeIn();
    }
    public void FadeIn(float duration = -1f)
    {
        StartCoroutine(Fade(1f, 0f, duration < 0 ? defaultFadeDuration : duration));
    }

    public void FadeOut(float duration = -1f)
    {
        StartCoroutine(Fade(0f, 1f, duration < 0 ? defaultFadeDuration : duration));
    }

    private IEnumerator Fade(float from, float to, float duration)
    {
        float elapsed = 0f;
        Color color = fadeImage.color;
        color.a = from;
        fadeImage.color = color;
        fadeImage.gameObject.SetActive(true);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Lerp(from, to, elapsed / duration);
            fadeImage.color = color;
            yield return null;
        }

        color.a = to;
        fadeImage.color = color;
        if (to == 0f)
            fadeImage.gameObject.SetActive(false);
    }
}