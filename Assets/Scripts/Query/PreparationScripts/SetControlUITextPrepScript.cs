using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetControlUITextPrepScript : AbstractPreparationScript
{
    public TextMeshProUGUI tmproText;
    public string text;

    public override void DoPreparation()
    {
        tmproText.text = "Steuerung:\n" + text;
    }
}
