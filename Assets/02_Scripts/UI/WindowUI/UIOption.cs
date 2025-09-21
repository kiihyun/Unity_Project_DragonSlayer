using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIOption : BaseWindow
{
    [SerializeField] private Button MainMenuButon;
    [SerializeField] private Slider SouondSlider;
    public override UIType UIType => UIType.UIOption;

    private void Start()
    {
        SouondSlider.onValueChanged.AddListener(OnSliderValueChanged);
    }
    
    public void OnMainMenuButton()
    {
        SceneManager.LoadScene("StartScene");
    }
    
    public void OnSliderValueChanged(float value)
    {
        // Log10 변환(-80~0dB) : 0.0001 -> -80, 1 -> 0, 데시벨이 로그 스케일이기 때문
        // .SetFloat("BGM", Mathf.Log10(value) * 20);
    }
    
    
}
