using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIQueryToggle : AbstractUIQuery
{
    public string query = "Gender";
    public string queryEng = "Gender";
    public Toggle toggle;
    public string text = "Gender";
    public string textEng = "Gender";

    void Awake()
    {
        GetComponentInChildren<Text>().text = LanguageSelection.lang == 0 ? text : textEng;

        toggle = GetComponentInChildren<Toggle>();
        toggle.onValueChanged.RemoveAllListeners();
        toggle.onValueChanged.AddListener(e =>
        {
            if (GetComponentInParent<UIQueryParent>())
            {
                GetComponentInParent<UIQueryParent>().NotifyChange();
            }
        });
        GetComponent<TextMeshProUGUI>().text = (LanguageSelection.lang == 0 ? query : queryEng) + (required ? "*" : "");
    }

    public override AbstractUIQueryResult GetResult()
    {
        AbstractUIQueryResult result = new AbstractUIQueryResult();
        result.query = query;
        result.result = "" + toggle.isOn;
        return result;
    }

    public override bool IsAccepted()
    {
        return toggle.isOn;
    }
}
