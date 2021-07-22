using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetTaskPrepScript : AbstractPreparationScript
{
    public string name = "";
    public string nameEng = "";

    public TextMeshProUGUI task;

    public override void DoPreparation()
    {
        task.text = LanguageSelection.lang == 0 ? name : nameEng;
    }
}
