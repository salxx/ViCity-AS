using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetAvatarsPrepScript : AbstractPreparationScript
{
    public GameObject avatarsParent;

    public override void DoPreparation()
    {
        avatarsParent.SetActive(true);
    }
}
