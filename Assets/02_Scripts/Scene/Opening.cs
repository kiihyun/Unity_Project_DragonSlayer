using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Opening : MonoBehaviour
{
    private Image _image;
    private float _timer;
    // Start is called before the first frame update
    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    private void Start()
    {
        StartCoroutine(Fade(1f, 0f));
    }

    private void Update()
    {
        if(_image.color.a==0f)
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    public void FadeIn()
    {
        StartCoroutine(Fade(1f, 0f));
    }


    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float timer = 0f;
        Color color = _image.color;

        while (timer < 1.5f)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, timer / 1.5f);
            _image.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }
        _image.color = new Color(color.r, color.g, color.b, endAlpha);
    }
}
