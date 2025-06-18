using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private LoadManger _loadManger;
    private UIManager _uiManager;
    
    private void Awake()
    {
        SoundManager.Instance.StopBGM();
        SoundManager.Instance.PlayBGM("BGM", true);
    }
    private void Start()
    {
        _loadManger = LoadManger.Instance;

        if(_loadManger != null && _loadManger.IsLoadGame == true)
        {
            _loadManger.LoadPlayer();
        }

        if (_uiManager == null)
        {
            _uiManager = UIManager.instance;
        }

        _uiManager.StartGameUISetting();
    }
}