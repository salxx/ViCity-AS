using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExplorationTaskPrepScript : AbstractPreparationScript
{
    public GameObject exitZone;
    public Text infoNextButton;

    public override void DoPreparation()
    {
        exitZone.SetActive(false);
        infoNextButton.text = LanguageSelection.lang == 0 ? "Zur Bewertung" : "To the Rating";
    }
}
