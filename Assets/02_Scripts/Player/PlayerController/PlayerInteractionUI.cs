using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInteractionUI : MonoBehaviour
{
    private Player _player;

    public void Start()
    {
        _player = GetComponent<Player>();
        HideInteractionUI();
    }


    [SerializeField] private TextMeshProUGUI interactionText;   

    private void Update()
    {
        if(_player.Controller.InteractObject != null)
        {
            ShowInteractionUI();
        }
        else
        {
            HideInteractionUI();
        }
    }

    public void ShowInteractionUI()
    {

        string interactText = _player.Controller.InteractObject.GetInteractText();
        SetInteractionText(interactText);
        
        interactionText.gameObject.SetActive(true);
        interactionText.transform.position = Camera.main.WorldToScreenPoint(_player.transform.position + new Vector3(0, 1, 0));
    }

    public void HideInteractionUI()
    {
        if(interactionText != null)
        {
            interactionText.gameObject.SetActive(false);
        }
    }

    public void SetInteractionText(string interactionText)
    {
        if(interactionText == null || interactionText == "")
        {
            this.interactionText.text = "E를 눌러 상호작용";
            return;
        }

        this.interactionText.text = interactionText;
    }
}
