using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : Interaction
{
    [SerializeField] private GameObject offSprite;
    [SerializeField] private GameObject onSprite;
    [SerializeField] private List<MonoBehaviour> _interactObjects;
    
    private bool isOn = false;
    private void Awake()
    {
        // 시작 시 닫힌 상태로 초기화
        SetSpriteState(isOn);
    }

    public override void Interact()
    {
        if (!isOn)
            OnLever();
        else
            OffLever();
    }

    public virtual void OffLever()
    {
        isOn = false;
        SetSpriteState(isOn);
        IsInteractable = true;
    }

    public virtual void OnLever()
    {
        isOn = true;
        SetSpriteState(isOn);
        IsInteractable = false;

        if(_interactObjects.Count > 0)
    {
            foreach(var interactObject in _interactObjects)
            {
                var target = interactObject as IInteractableTarget;
                if(target != null)
                {
                    target.OnLeverActivated();
                }
            }
        }
    }

    private void SetSpriteState(bool on)
    {
        offSprite.SetActive(!on);
        onSprite.SetActive(on);
    }
}