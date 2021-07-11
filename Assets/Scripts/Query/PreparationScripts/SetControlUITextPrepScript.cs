using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetControlUITextPrepScript : AbstractPreparationScript
{
    public TextMeshProUGUI tmproText;
    public string text;
    public string textEng;

    public override void DoPreparation()
    {
        tmproText.text = LanguageSelection.lang == 0 ? ("Steuerung:\n" + text) : ("Controls:\n" + textEng);
    }
}
