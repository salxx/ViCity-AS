using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInputPrepScript : AbstractPreparationScript
{
    public bool allow = false;

    public override void DoPreparation() {
        FindObjectOfType<PlayerMoveScript>().ResetPosition();
        PlayerMoveScript playerMoveScript = FindObjectOfType<PlayerMoveScript>();
        playerMoveScript.allowKeys = allow;
    }
}
