using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneManager_StartScene : MonoBehaviour
{
    [SerializeField] private GameObject _textObj;
    [SerializeField] private GameObject _buttonObj;
    [SerializeField] private Image _fadeImage;
    [SerializeField] private float _fadeDuration;
    private bool _pressAnyButton = false;
    
       // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.PlayBGM("Rain", true);
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.anyKeyDown || Input.GetMouseButton(0))&& _pressAnyButton == false)
        {
            SoundManager.Instance.PlaySFX("PressAnyButton");
            _textObj.SetActive(false);
            _buttonObj.SetActive(true);
            _pressAnyButton = true;
        }

        if(_fadeImage.color.a == 1)
        {
            SceneManager.LoadScene("01_GameScene");
        }
    }

    public void FadeOut()
    {
        SoundManager.Instance.PlaySFX("GameStart");
        _fadeDuration *= 2f;
        StartCoroutine(Fade(0f, 1f));
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

    public void Newgame()
    {
        LoadManger.Instance.NewGame();
    }

    public void LoadGame()
    {
        LoadManger.Instance.LoadGame();
    }
}
