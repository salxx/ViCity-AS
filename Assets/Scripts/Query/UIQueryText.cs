using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UIQueryText : AbstractUIQuery
{
    public string queryText;
    public string queryTextEng;

    private void OnEnable()
    {
        GetComponent<TextMeshProUGUI>().text = LanguageSelection.lang == 0 ? queryText : queryTextEng;
    }

    public override AbstractUIQueryResult GetResult()
    {
        return null;
    }

    public override bool IsAccepted()
    {
        return true;
    }
}
