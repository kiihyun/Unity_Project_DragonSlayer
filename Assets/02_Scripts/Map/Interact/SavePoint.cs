using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : Interaction
{
    public override string GetInteractText()
    {
        return "저장하기";
    }
    public override void Interact()
    {
        SaveManager.Instance.SavePlayer();
    }
}