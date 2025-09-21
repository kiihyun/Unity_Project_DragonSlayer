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
        interactionText.gameObject.SetActive(true);
    }

    public void HideInteractionUI()
    {
        interactionText.gameObject.SetActive(false);
    }

    public void SetInteractionText(string interactionText)
    {
        this.interactionText.text = interactionText;
    }
}
