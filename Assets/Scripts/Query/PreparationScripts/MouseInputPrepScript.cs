using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInputPrepScript : AbstractPreparationScript
{
    public bool allow = false;

    public override void DoPreparation()
    {
        PlayerMoveScript playerMoveScript = FindObjectOfType<PlayerMoveScript>();
        playerMoveScript.allowMouse = allow;
    }
}
