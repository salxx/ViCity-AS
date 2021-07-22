using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetTitlePrepScript : AbstractPreparationScript
{
    public string name = "";
    public string nameEng = "";

    public TextMeshProUGUI title1;
    public TextMeshProUGUI title2;

    public override void DoPreparation()
    {
        title1.text = LanguageSelection.lang == 0 ? name : nameEng;
        title2.text = LanguageSelection.lang == 0 ? name : nameEng;
    }
}
