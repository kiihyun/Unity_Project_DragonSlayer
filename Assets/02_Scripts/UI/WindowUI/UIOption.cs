using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIOption : BaseWindow
{
    [SerializeField] private Button MainMenuButon;
    public override UIType UIType => UIType.UIOption;

    public void OnMainMenuButton()
    {
        SceneManager.LoadScene(Constants.Scene.START_SCENE);
    }

    
}
