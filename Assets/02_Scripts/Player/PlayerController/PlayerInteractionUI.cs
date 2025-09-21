using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInteractionUI : MonoBehaviour
{
    private Player _player;

    private GameObject _playerGetItemUI;

    public void Start()
    {
        _player = GetComponentInParent<Player>();
        HideInteractionUI();
    }


    [SerializeField] private TextMeshProUGUI interactionText;
    [SerializeField] private GameObject itemGetUIPrefab;

    public void ShowItemGetUI(ItemData itemData)
    {
        Vector3 position = Camera.main.WorldToScreenPoint(_player.transform.position + new Vector3(0, 1, 0));
        GameObject itemGetUI = Instantiate(itemGetUIPrefab, transform);
        itemGetUI.GetComponent<ItemGetUI>().ShowItems(itemData, position);
    }


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
