using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInputPrepScript : AbstractPreparationScript
{
    public bool allow = false;

    public override void DoPreparation() {
        PlayerMoveScript playerMoveScript = FindObjectOfType<PlayerMoveScript>();
        playerMoveScript.ResetPosition();
        playerMoveScript.allowKeys = allow;
    }
}
