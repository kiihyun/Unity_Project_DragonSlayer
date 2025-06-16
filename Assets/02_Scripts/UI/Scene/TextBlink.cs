using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextBlink : MonoBehaviour
{
    [SerializeField] private float duration;
    public TextMeshProUGUI MainText;
    private float _color_Alpha;
    
    // Start is called before the first frame update
    private void Awake()
    {
        MainText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
         _color_Alpha = Mathf.Sin(Time.time * Mathf.PI / duration) * 0.5f + 0.5f;

        MainText.color = new Color( 1,1,1 ,_color_Alpha); 
    }
}
