using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UIQueryText : AbstractUIQuery
{
    public string queryText;
    public string queryTextEng;

    [TextArea(3, 10)]
    public string queryDetails;
    [TextArea(3, 10)]
    public string queryDetailsEng;

    private void OnEnable()
    {
        GetComponent<TextMeshProUGUI>().text = LanguageSelection.lang == 0 ? queryText : queryTextEng;
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = LanguageSelection.lang == 0 ? queryDetails : queryDetailsEng;
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
