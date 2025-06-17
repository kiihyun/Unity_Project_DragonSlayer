using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManager_Intro1 : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _vCamera;
    [SerializeField] private CinemachineBasicMultiChannelPerlin _perlin;

    [SerializeField] private float shakeAmplitude;
    [SerializeField] private float shakeFrequency;
    [SerializeField] private Image _fadeImage;
    [SerializeField] private float _fadeDuration;
    // Start is called before the first frame update
    private void Awake()
    {
        _perlin = _vCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        ShakeOff();
    }

    private void Start()
    {
        FadeIn();
        Invoke(nameof(ShakeOn),1.5f);
    }
    public void ShakeOn()
    {
        _perlin.m_AmplitudeGain = shakeAmplitude;
        _perlin.m_FrequencyGain = shakeFrequency;
    }

    public void ShakeOff()
    {
        _perlin.m_AmplitudeGain = 0f;
        _perlin.m_FrequencyGain = 0f;
    }


    // Update is called once per frame
    void Update()
    {
        if (_fadeImage.color.a == 1)
        {
            Debug.Log("SceneChange");
            SceneManager.LoadScene("Intro2");
        }
    }
    public void FadeOut()
    {
        _fadeDuration *= 2f;
        StartCoroutine(Fade(0f, 1f));
    }

    public void FadeIn()
    {
        StartCoroutine(Fade(1f, 0f));
    }

    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float timer = 0f;
        Color color = _fadeImage.color;

        while (timer < _fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, timer / _fadeDuration);
            _fadeImage.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }
        _fadeImage.color = new Color(color.r, color.g, color.b, endAlpha);
    }
}
