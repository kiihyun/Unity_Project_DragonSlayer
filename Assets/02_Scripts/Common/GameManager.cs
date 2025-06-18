using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private LoadManger _loadManger;
    private void Start()
    {
        _loadManger = LoadManger.Instance;

        if(_loadManger != null && _loadManger.IsLoadGame == true)
        {
            //_loadManger.LoadPlayer();
        }
    }
}