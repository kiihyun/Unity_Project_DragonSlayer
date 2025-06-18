using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : Interaction
{
    public override string GetInteractText()
    {
        return Constants.Interaction.SAVE_POINT_INTERACT_TEXT;
    }
    public override void Interact()
    {
        SaveManager.Instance.SavePlayer();
    }
}