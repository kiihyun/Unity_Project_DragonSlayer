using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManager_Town : MonoBehaviour
{
    [SerializeField] private Transform _backGround;
    [SerializeField] private CinemachineVirtualCamera _vCamera;
    [SerializeField] private Image _image;
    [SerializeField] private float shakeAmplitude;
    [SerializeField] private float shakeFrequency;
    [SerializeField] private float _fadeDuration;

    private CinemachineBasicMultiChannelPerlin _perlin;
    // Start is called before the first frame update
    private void Awake()
    {
        _perlin =  _vCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }
    private void Start()
    {
        ShakeOff();
        FadeIn();
        Invoke(nameof(ShakeOn), 2f);
    }
    private void Update()
    {
        _backGround.transform.localPosition += Vector3.left * 0.0005f;
        if (_backGround.transform.localPosition.x <= -3.85f)
            _backGround.transform.localPosition = new Vector3(0,0,0);


        if(_image.color.a == 1)
        {
            Debug.Log("SceneChange");
            SoundManager.Instance.StopBGM();
            SceneManager.LoadScene("StartScene");
        }
    }



    public void ShakeOn()
    {
        SoundManager.Instance.PlayBGM("NewExplode", false);
        _perlin.m_AmplitudeGain = shakeAmplitude;
        _perlin.m_FrequencyGain = shakeFrequency;
        Invoke(nameof(FadeOut), 2f);
    }


    public void ShakeOff()
    {
        _perlin.m_AmplitudeGain = 0f;
        _perlin.m_FrequencyGain = 0f;
    }


    public void FadeOut()
    {
        _fadeDuration *= 3f;
        _image.color = Color.white;
        StartCoroutine(Fade(0f, 1f));
    }


    public void FadeIn()
    {
        StartCoroutine(Fade(1f, 0f));
    }


    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float timer = 0f;
        Color color = _image.color;

        while (timer < _fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, timer / _fadeDuration);
            _image.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }
        _image.color = new Color(color.r, color.g, color.b, endAlpha);
    }
}
